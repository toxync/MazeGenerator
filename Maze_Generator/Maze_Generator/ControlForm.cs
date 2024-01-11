using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Maze_Generator
{
    public partial class ControlForm : Form
    {
        private MazeForm subForm;
        private Random rand;
        private int Random_Walk_Start_X, Random_Walk_Start_Y;
        private int Random_Walk_Current_X, Random_Walk_Current_Y;
        private int Random_Walk_Previous_X, Random_Walk_Previous_Y;
        private int Random_Move;
        public int CellSize = 16;
        public int CellCnt = 32;
        public int Spacing;
        public Point Route_Start_Pos, Route_End_Pos;
        public bool Is_Maze_Ready;
        private byte[,] Wall_Position_Of_Cell = new byte[32, 32];
        private bool[,] Is_Cell_In_Maze = new bool[32, 32];
        private int[,] Random_Walk_Direction = new int[32, 32];
        private List<Point> Opened_Cells = new List<Point>();
        private List<Point> Closed_Cells = new List<Point>();
        private List<Point> Route = new List<Point>();
        private Point[,] Parent_Of_Node = new Point[32, 32];
        private int[,] F_Value_Of_Node = new int[32, 32];
        private int[,] G_Value_Of_Node = new int[32, 32];
        private int[,] H_Value_Of_Node = new int[32, 32];
        private enum Movement
        {
            Up = 1,
            Down,
            Left,
            Right
        }
        private enum Wall_Position
        {
            Up = 8,
            Down = 4,
            Left = 2,
            Right = 1
        }

        public ControlForm()
        {
            InitializeComponent();
            ControlForm mainForm = this;
            mainForm.StartPosition = FormStartPosition.Manual;
            mainForm.Location = new Point(800, 250);
            subForm = new MazeForm(this);
            subForm.StartPosition = FormStartPosition.Manual;
            subForm.Location = new Point(this.Location.X - 600, this.Location.Y - 100);
            subForm.Show();
            rand = new Random();
            Spacing = (subForm.MazePicture.ClientSize.Height - (CellSize * CellCnt)) / 2;
            Maze_Init();
        }

        private void CreateMaze_Click(object sender, EventArgs e)
        {
            Generate_Maze(); // Wilson's Algorithm을 사용하여 미로 생성
            ResetMaze.Enabled = true;
            SaveMazeData.Enabled = true;
        }

        private void ResetMaze_Click(object sender, EventArgs e)
        {
            Maze_Init();
        }

        private void Reset_Pict_Box()
        {
            if (subForm.MazePicture.Image != null)
            {
                subForm.MazePicture.Image.Dispose();
                subForm.MazePicture.Image = null;
            }
            subForm.MazePicture.BackColor = Color.White;
            subForm.MazePicture.Refresh();
        }

        private void Maze_Init()
        {
            for (int i = 0; i < CellCnt; ++i)
            {
                for (int j = 0; j < CellCnt; ++j)
                {
                    Is_Cell_In_Maze[i, j] = false;
                    Wall_Position_Of_Cell[i, j] = 15;
                    Random_Walk_Direction[i, j] = 0;
                }
            }
            Reset_Route_Info();
            Route_Start_Pos = new Point(-1, -1);
            Route_End_Pos = new Point(-1, -1);
            Is_Cell_In_Maze[rand.Next(CellCnt), rand.Next(CellCnt)] = true;
            Is_Maze_Ready = false;
            ResetMaze.Enabled = false;
            SaveMazeData.Enabled = false;
            ResetRoutePoints.Enabled = false;
            FindRoute.Enabled = false;
            ShowRouteAnimation.Enabled = false;
            Reset_Pict_Box();
        }

        private void Generate_Maze()
        {
            bool Do_Not_Update;
            Maze_Init();
            // 모든 칸들이 미로에 포함될 때까지 반복
            while (!Is_All_Cells_In_Maze())
            {
                // 미로에 포함되지 않은 임의의 칸 선택
                do
                {
                    Random_Walk_Start_X = rand.Next(CellCnt);
                    Random_Walk_Start_Y = rand.Next(CellCnt);
                } while (Is_Cell_In_Maze[Random_Walk_Start_Y, Random_Walk_Start_X]);
                Random_Walk_Current_X = Random_Walk_Start_X;
                Random_Walk_Current_Y = Random_Walk_Start_Y;
                // 선택한 임의의 칸에서 미로에 포함된 칸에 도달할 때까지 상하좌우 중 무작위 방향으로 이동하면서 동선 기록
                do
                {
                    // 이동 결과는 미로 안이어야 함
                    do
                    {
                        Do_Not_Update = false;
                        // 이동 결과가 무효한 위치인지 판정
                        if (Random_Walk_Current_X < 0 || Random_Walk_Current_X >= CellCnt ||
                        Random_Walk_Current_Y < 0 || Random_Walk_Current_Y >= CellCnt)
                        {
                            Do_Not_Update = true;
                        }
                        if (!Do_Not_Update) // 이동 결과가 유효한 위치라면 위치 갱신
                        {
                            Random_Walk_Previous_X = Random_Walk_Current_X;
                            Random_Walk_Previous_Y = Random_Walk_Current_Y;
                        }
                        else // 이동 결과가 무효한 위치라면 이동 전 상태로 롤백
                        {
                            Random_Walk_Current_X = Random_Walk_Previous_X;
                            Random_Walk_Current_Y = Random_Walk_Previous_Y;
                        }
                        // 상하좌우 중 무작위 방향으로 이동
                        Random_Move = rand.Next(1, 5);
                        switch (Random_Move)
                        {
                            case (int)Movement.Up:
                                --Random_Walk_Current_Y;
                                break;
                            case (int)Movement.Down:
                                ++Random_Walk_Current_Y;
                                break;
                            case (int)Movement.Left:
                                --Random_Walk_Current_X;
                                break;
                            case (int)Movement.Right:
                                ++Random_Walk_Current_X;
                                break;
                        }
                    } while (Random_Walk_Current_X < 0 || Random_Walk_Current_X >= CellCnt ||
                        Random_Walk_Current_Y < 0 || Random_Walk_Current_Y >= CellCnt);
                    // 이동 후 동선 기록
                    Random_Walk_Direction[Random_Walk_Previous_Y, Random_Walk_Previous_X] = Random_Move;
                } while (!Is_Cell_In_Maze[Random_Walk_Current_Y, Random_Walk_Current_X]);
                // 다시 임의로 선택한 칸으로 돌아가서
                Random_Walk_Current_X = Random_Walk_Start_X;
                Random_Walk_Current_Y = Random_Walk_Start_Y;
                // 임의의 칸에서 미로에 포함된 칸에 도착할 때까지 기록해둔 동선을 따라가면서 경로에 존재하는 칸들을 미로에 추가
                do
                {
                    // 경로에 존재하는 칸을 미로에 추가
                    Is_Cell_In_Maze[Random_Walk_Current_Y, Random_Walk_Current_X] = true;
                    // 이동하기 전의 칸과 이동한 후의 칸 사이에 통로가 있음을 표시하기 위해 두 칸의 맞닿은 벽 제거
                    switch (Random_Walk_Direction[Random_Walk_Current_Y, Random_Walk_Current_X])
                    {
                        case (int)Movement.Up:
                            Wall_Position_Of_Cell[Random_Walk_Current_Y, Random_Walk_Current_X] ^= (byte)Wall_Position.Up;
                            --Random_Walk_Current_Y;
                            Wall_Position_Of_Cell[Random_Walk_Current_Y, Random_Walk_Current_X] ^= (byte)Wall_Position.Down;
                            break;
                        case (int)Movement.Down:
                            Wall_Position_Of_Cell[Random_Walk_Current_Y, Random_Walk_Current_X] ^= (byte)Wall_Position.Down;
                            ++Random_Walk_Current_Y;
                            Wall_Position_Of_Cell[Random_Walk_Current_Y, Random_Walk_Current_X] ^= (byte)Wall_Position.Up;
                            break;
                        case (int)Movement.Left:
                            Wall_Position_Of_Cell[Random_Walk_Current_Y, Random_Walk_Current_X] ^= (byte)Wall_Position.Left;
                            --Random_Walk_Current_X;
                            Wall_Position_Of_Cell[Random_Walk_Current_Y, Random_Walk_Current_X] ^= (byte)Wall_Position.Right;
                            break;
                        case (int)Movement.Right:
                            Wall_Position_Of_Cell[Random_Walk_Current_Y, Random_Walk_Current_X] ^= (byte)Wall_Position.Right;
                            ++Random_Walk_Current_X;
                            Wall_Position_Of_Cell[Random_Walk_Current_Y, Random_Walk_Current_X] ^= (byte)Wall_Position.Left;
                            break;
                    }
                } while (!Is_Cell_In_Maze[Random_Walk_Current_Y, Random_Walk_Current_X]);
            }
            Draw_Maze();
            Is_Maze_Ready = true;
        }

        private bool Is_All_Cells_In_Maze()
        {
            bool result = true;
            for (int i = 0; i < CellCnt; ++i)
            {
                for (int j = 0; j < CellCnt; ++j)
                {
                    if (!Is_Cell_In_Maze[i, j])
                    {
                        result = false;
                        break;
                    }
                }
                if (!result)
                {
                    break;
                }
            }
            return result;
        }

        private void Draw_Maze()
        {
            Reset_Pict_Box();
            Bitmap bmp = new Bitmap(subForm.MazePicture.ClientSize.Width, subForm.MazePicture.ClientSize.Height);
            bmp = Create_Refresh_Image(bmp);
            if (subForm.MazePicture.Image != null)
            {
                subForm.MazePicture.Image.Dispose();
                subForm.MazePicture.Image = null;
            }
            subForm.MazePicture.Image = bmp;
            subForm.MazePicture.Refresh();
            ResetMaze.Enabled = true; // 미로가 그려졌으니 미로 초기화 기능 활성화
        }

        public void Draw_Cell(int y, int x, byte wall, Graphics graphics)
        {
            // 위쪽 벽이 존재할 경우
            if ((wall & (byte)Wall_Position.Up) == (byte)Wall_Position.Up)
            {
                graphics.DrawLine(Pens.Black, (float)Spacing + CellSize * x, (float)Spacing + CellSize * y,
                    (float)Spacing + CellSize * (x + 1), (float)Spacing + CellSize * y);
            }
            // 아래쪽 벽이 존재할 경우
            if ((wall & (byte)Wall_Position.Down) == (byte)Wall_Position.Down)
            {
                graphics.DrawLine(Pens.Black, (float)Spacing + CellSize * x, (float)Spacing + CellSize * (y + 1),
                    (float)Spacing + CellSize * (x + 1), (float)Spacing + CellSize * (y + 1));
            }
            // 왼쪽 벽이 존재할 경우
            if ((wall & (byte)Wall_Position.Left) == (byte)Wall_Position.Left)
            {
                graphics.DrawLine(Pens.Black, (float)Spacing + CellSize * x, (float)Spacing + CellSize * y,
                    (float)Spacing + CellSize * x, (float)Spacing + CellSize * (y + 1));
            }
            // 오른쪽 벽이 존재할 경우
            if ((wall & (byte)Wall_Position.Right) == (byte)Wall_Position.Right)
            {
                graphics.DrawLine(Pens.Black, (float)Spacing + CellSize * (x + 1), (float)Spacing + CellSize * y,
                    (float)Spacing + CellSize * (x + 1), (float)Spacing + CellSize * (y + 1));
            }
        }

        public Bitmap Create_Refresh_Image(Bitmap bmp)
        {
            using (Graphics graphics = Graphics.FromImage(bmp))
            {
                using (SolidBrush white = new SolidBrush(Color.White))
                {
                    graphics.FillRectangle(white, 0, 0, subForm.MazePicture.ClientSize.Width, subForm.MazePicture.ClientSize.Height);
                }
                for (int i = 0; i < CellCnt; ++i)
                {
                    for (int j = 0; j < CellCnt; ++j)
                    {
                        Draw_Cell(i, j, Wall_Position_Of_Cell[i, j], graphics);
                    }
                }
                graphics.Dispose();
            }
            return bmp;
        }

        private void SaveMazeData_Click(object sender, EventArgs e)
        {
            // 저장 다이얼로그의 최초 경로가 이 프로젝트 솔루션의 경로가 되도록 지정
            string[] splits = Application.StartupPath.Split('\\');
            StringBuilder Path = new StringBuilder();
            for (int i = 0; i < splits.Length - 3; ++i)
            {
                Path.Append(splits[i]);
                if (i < splits.Length - 3)
                {
                    Path.Append("\\");
                }
            }
            string fileName;            
            using (SaveFileDialog saveFile = new SaveFileDialog())
            {
                saveFile.InitialDirectory = Path.ToString();
                saveFile.Title = "미로 데이터 저장 위치 지정";
                saveFile.Filter = "텍스트 문서(*.txt)|*.txt|모든 파일(*.*)|*.*";
                saveFile.DefaultExt = "txt";
                saveFile.OverwritePrompt = true;
                if (saveFile.ShowDialog() == DialogResult.OK)
                {
                    fileName = saveFile.FileName;
                    StringBuilder MazeData = new StringBuilder();
                    for (int i = 0; i < CellCnt; ++i)
                    {
                        for (int j = 0; j < CellCnt; ++j)
                        {
                            MazeData.Append(Wall_Position_Of_Cell[i, j]);
                            if (j < CellCnt - 1)
                            {
                                MazeData.Append("|");
                            }
                        }
                        MazeData.Append("\n");
                    }
                    File.AppendAllText(fileName, MazeData.ToString());
                }
            }
        }

        private void ResetRoutePoints_Click(object sender, EventArgs e)
        {
            Route_Start_Pos = new Point(-1, -1);
            Route_End_Pos = new Point(-1, -1);
            // 출발점과 도착점이 초기화됐으므로 출발점 & 도착점 초기화 기능과 경로 탐색 기능, 경로 애니메이션 재생 기능 비활성화
            ResetRoutePoints.Enabled = false;
            FindRoute.Enabled = false;
            ShowRouteAnimation.Enabled = false;
            Bitmap bmp = new Bitmap(subForm.MazePicture.ClientSize.Width, subForm.MazePicture.ClientSize.Height);
            bmp = Create_Refresh_Image(bmp);
            if (subForm.MazePicture.Image != null)
            {
                subForm.MazePicture.Image.Dispose();
                subForm.MazePicture.Image = null;
            }
            subForm.MazePicture.Image = bmp;
        }

        private void LoadMazeData_Click(object sender, EventArgs e)
        {
            Maze_Init();
            char[] delimiterChars = { '|', '\n' };
            string[] MazeData = File.ReadAllText(MazePathList.SelectedItem.ToString()).Split(delimiterChars);
            for (int i = 0; i < CellCnt; ++i)
            {
                for (int j = 0; j < CellCnt; ++j)
                {
                    Wall_Position_Of_Cell[i, j] = (byte)Convert.ToInt32(MazeData[i * CellCnt + j]);
                }
            }
            Draw_Maze();
            Is_Maze_Ready = true;
        }

        private void AddMazeToList_Click(object sender, EventArgs e)
        {
            // 불러오기 다이얼로그의 최초 경로가 이 프로젝트 솔루션의 경로가 되도록 지정
            string[] splits = Application.StartupPath.Split('\\');
            StringBuilder filePath = new StringBuilder();
            for (int i = 0; i < splits.Length - 3; ++i)
            {
                filePath.Append(splits[i]);
                if (i < splits.Length - 3)
                {
                    filePath.Append("\\");
                }
            }            
            using (OpenFileDialog loadFile = new OpenFileDialog())
            {
                loadFile.InitialDirectory = filePath.ToString();
                loadFile.Title = "불러올 미로 데이터 선택";
                loadFile.Filter = "텍스트 문서(*.txt)|*.txt|모든 파일(*.*)|*.*";
                loadFile.DefaultExt = "txt";
                loadFile.Multiselect = true;
                if (loadFile.ShowDialog() == DialogResult.OK)
                {
                    for (int i = 0; i < loadFile.FileNames.Length; ++i)
                    {
                        // 불러오고자 하는 미로 데이터가 리스트박스에 이미 있다면 불러오지 않음
                        if (MazeList.Items.Contains(Path.GetFileNameWithoutExtension(loadFile.FileNames[i])))
                        {
                            continue;
                        }
                        MazeList.Items.Add(Path.GetFileNameWithoutExtension(loadFile.FileNames[i]));
                        MazePathList.Items.Add(loadFile.FileNames[i]);
                    }
                }
            }
        }

        private void DeleteMazeFromList_Click(object sender, EventArgs e)
        {
            int cnt = MazeList.SelectedIndices.Count;
            for (int i = cnt - 1; i >= 0; --i)
            {
                // 미로 데이터의 이름 리스트박스를 기준으로 두 리스트박스가 연동되므로
                // 미로 데이터의 절대 경로 리스트박스 항목을 먼저 지워야 함
                MazePathList.Items.RemoveAt(MazePathList.SelectedIndices[i]);
                MazeList.Items.RemoveAt(MazeList.SelectedIndices[i]);
            }            
        }

        private void MazeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int cnt = MazeList.SelectedIndices.Count;
            if (cnt == 1) // 미로 데이터 리스트박스의 선택 항목이 하나라면 불러오기와 삭제 활성화
            {
                LoadMazeData.Enabled = true;
                DeleteMazeFromList.Enabled = true;
            }
            else if (cnt > 1) // 미로 데이터 리스트박스의 선택 항목이 하나보다 많다면 삭제만 활성화
            {
                LoadMazeData.Enabled = false;
                DeleteMazeFromList.Enabled = true;
            }
            else // 미로 데이터 리스트박스의 선택 항목이 없다면 불러오기와 삭제 모두 비활성화
            {
                LoadMazeData.Enabled = false;
                DeleteMazeFromList.Enabled = false;
            }
            // 미로 데이터 리스트박스의 선택 항목이 변동되었다면 
            // 미로 데이터의 절대 경로 리스트박스 선택 항목을 모두 지우고 선택 항목을 반영하여 두 리스트박스를 연동
            MazePathList.ClearSelected();
            for (int i = 0; i < cnt; ++i)
            {
                MazePathList.SetSelected(MazeList.SelectedIndices[i], true);
            }
        }

        private void FindRoute_Click(object sender, EventArgs e)
        {
            Reset_Route_Info();
            Closed_Cells.Add(Route_Start_Pos); // 출발점을 닫힌 칸 목록에 저장
            int Moved_Row, Moved_Col;
            for (int i = -1; i <= 1; ++i)
            {
                for (int j = -1; j <= 1; ++j)
                {
                    if (i * j != 0) // 상하좌우로 인접한 칸에 대해서만 열린 칸으로 고려(대각선으로 인접한 칸은 고려하지 않음)
                    {
                        continue;
                    }
                    Moved_Row = Route_Start_Pos.X + i;
                    Moved_Col = Route_Start_Pos.Y + j;
                    // 인접한 새로운 칸이 미로 안의 유효한 위치인지 확인
                    if (Moved_Row < 0 || Moved_Row >= CellCnt || Moved_Col < 0 || Moved_Col >= CellCnt)
                    {
                        continue;
                    }
                    // 인접한 새로운 칸을 출발점 위로 잡았을 때 새로운 칸의 아래가 벽으로 막혔다면 열린 칸에서 제외
                    if (i == -1 && j == 0 && (Wall_Position_Of_Cell[Moved_Row, Moved_Col] & (byte)Wall_Position.Down) == 4)
                    {
                        continue;
                    }
                    // 인접한 새로운 칸을 출발점 아래로 잡았을 때 새로운 칸의 위가 벽으로 막혔다면 열린 칸에서 제외
                    if (i == 1 && j == 0 && (Wall_Position_Of_Cell[Moved_Row, Moved_Col] & (byte)Wall_Position.Up) == 8)
                    {
                        continue;
                    }
                    // 인접한 새로운 칸을 출발점 왼쪽으로 잡았을 때 새로운 칸의 오른쪽이 벽으로 막혔다면 열린 칸에서 제외
                    if (i == 0 && j == -1 && (Wall_Position_Of_Cell[Moved_Row, Moved_Col] & (byte)Wall_Position.Right) == 1)
                    {
                        continue;
                    }
                    // 인접한 새로운 칸을 출발점 오른쪽으로 잡았을 때 새로운 칸의 왼쪽이 벽으로 막혔다면 열린 칸에서 제외
                    if (i == 0 && j == 1 && (Wall_Position_Of_Cell[Moved_Row, Moved_Col] & (byte)Wall_Position.Left) == 2)
                    {
                        continue;
                    }
                    Opened_Cells.Add(new Point(Moved_Row, Moved_Col)); // 위의 조건들을 모두 만족하는 새로운 칸을 열린 칸 목록에 저장
                    Parent_Of_Node[Moved_Row, Moved_Col] = Route_Start_Pos; // 새로운 칸의 부모 노드를 출발점으로 지정
                    G_Value_Of_Node[Moved_Row, Moved_Col] = 10; // 새로운 칸은 출발점의 상하좌우로 인접해 있으므로 G 값은 10
                    H_Value_Of_Node[Moved_Row, Moved_Col] = 10 * (Math.Abs(Route_End_Pos.X - Moved_Row) + Math.Abs(Route_End_Pos.Y - Moved_Col));
                    F_Value_Of_Node[Moved_Row, Moved_Col] = 10 + H_Value_Of_Node[Moved_Row, Moved_Col]; // F = G + H
                }
            }
            Point Current;
            bool No_Route = false;
            while (!Opened_Cells.Contains(Route_End_Pos))
            {
                if (Opened_Cells.Count == 0) // 열린 칸이 더 이상 없다면 경로가 없음
                {
                    MessageBox.Show("경로 없음");
                    No_Route = true;
                    break;
                }
                // F 값이 가장 작은 열린 칸을 선택하여 열린 칸 목록에서 제거하고 닫힌 칸 목록에 넣음
                Current = Find_Smallest_F_In_Opened_Cells();
                Opened_Cells.RemoveAt(Opened_Cells.IndexOf(Current));
                Closed_Cells.Add(Current);
                for (int i = -1; i <= 1; ++i)
                {
                    for (int j = -1; j <= 1; ++j)
                    {
                        if (i * j != 0) // 상하좌우로 인접한 칸에 대해서만 열린 칸으로 고려(대각선으로 인접한 칸은 고려하지 않음)
                        {
                            continue;
                        }
                        Moved_Row = Current.X + i;
                        Moved_Col = Current.Y + j;
                        // 인접한 새로운 칸이 미로 안의 유효한 위치인지 확인
                        if (Moved_Row < 0 || Moved_Row >= CellCnt || Moved_Col < 0 || Moved_Col >= CellCnt)
                        {
                            continue;
                        }
                        // 인접한 새로운 칸이 닫힌 칸 목록에 있다면 건너뜀
                        if (Closed_Cells.Contains(new Point(Moved_Row, Moved_Col)))
                        {
                            continue;
                        }
                        // 인접한 새로운 칸을 현재 칸 위로 잡았을 때 새로운 칸의 아래가 벽으로 막혔다면 열린 칸에서 제외
                        if (i == -1 && j == 0 && (Wall_Position_Of_Cell[Moved_Row, Moved_Col] & (byte)Wall_Position.Down) == 4)
                        {
                            continue;
                        }
                        // 인접한 새로운 칸을 현재 칸 아래로 잡았을 때 새로운 칸의 위가 벽으로 막혔다면 열린 칸에서 제외
                        if (i == 1 && j == 0 && (Wall_Position_Of_Cell[Moved_Row, Moved_Col] & (byte)Wall_Position.Up) == 8)
                        {
                            continue;
                        }
                        // 인접한 새로운 칸을 현재 칸 왼쪽으로 잡았을 때 새로운 칸의 오른쪽이 벽으로 막혔다면 열린 칸에서 제외
                        if (i == 0 && j == -1 && (Wall_Position_Of_Cell[Moved_Row, Moved_Col] & (byte)Wall_Position.Right) == 1)
                        {
                            continue;
                        }
                        // 인접한 새로운 칸을 현재 칸 오른쪽으로 잡았을 때 새로운 칸의 왼쪽이 벽으로 막혔다면 열린 칸에서 제외
                        if (i == 0 && j == 1 && (Wall_Position_Of_Cell[Moved_Row, Moved_Col] & (byte)Wall_Position.Left) == 2)
                        {
                            continue;
                        }
                        // 인접한 새로운 칸이 열린 칸 목록에 이미 있고
                        if (Opened_Cells.Contains(new Point(Moved_Row, Moved_Col)))
                        {
                            // 인접한 새로운 칸의 기존 G 값보다 현재 칸을 부모로 삼았을 때의 G 값이 더 작다면
                            if (G_Value_Of_Node[Current.X, Current.Y] + 10 < G_Value_Of_Node[Moved_Row, Moved_Col])
                            {
                                // 인접한 새로운 칸의 부모를 현재 칸으로 변경하고 그에 맞게 G 값과 F 값을 수정
                                Parent_Of_Node[Moved_Row, Moved_Col] = Current;
                                G_Value_Of_Node[Moved_Row, Moved_Col] = G_Value_Of_Node[Current.X, Current.Y] + 10;
                                F_Value_Of_Node[Moved_Row, Moved_Col] = G_Value_Of_Node[Moved_Row, Moved_Col] + H_Value_Of_Node[Moved_Row, Moved_Col];
                            }
                        }
                        else
                        {
                            Opened_Cells.Add(new Point(Moved_Row, Moved_Col)); // 위의 조건들을 모두 만족하는 새로운 칸을 열린 칸 목록에 저장
                            Parent_Of_Node[Moved_Row, Moved_Col] = Current; // 새로운 칸의 부모 노드를 현재 칸으로 지정
                            G_Value_Of_Node[Moved_Row, Moved_Col] = 10; // 새로운 칸은 현재 칸의 상하좌우로 인접해 있으므로 G 값은 10
                            H_Value_Of_Node[Moved_Row, Moved_Col] = 10 * (Math.Abs(Current.X - Moved_Row) + Math.Abs(Current.Y - Moved_Col));
                            F_Value_Of_Node[Moved_Row, Moved_Col] = 10 + H_Value_Of_Node[Moved_Row, Moved_Col]; // F = G + H
                        }
                    }
                }
            }
            if (No_Route) // 경로가 없다면 경로 저장 과정 생략
            {
                return;
            }
            Current = Route_End_Pos; // 경로의 도착점부터 부모를 찾아가며 경로 저장
            do
            {
                Route.Add(Current);
                Current = Parent_Of_Node[Current.X, Current.Y];
            } while (Current != Route_Start_Pos);
            Route.Add(Route_Start_Pos); // 마지막으로 경로의 출발점을 경로에 저장한 뒤
            Route.Reverse(); // 경로의 출발점이 앞으로 오도록 역순 정렬
            int center_x, center_y, next_center_x, next_center_y;
            Bitmap bmp = (Bitmap)subForm.MazePicture.Image;
            for (int i = 0; i < Route.Count - 1; ++i) // 경로에 포함되는 칸들을 선으로 이으며 경로 표기
            {
                center_x = Spacing + Route[i].Y * CellSize + CellSize / 2;
                center_y = Spacing + Route[i].X * CellSize + CellSize / 2;
                next_center_x = Spacing + Route[i + 1].Y * CellSize + CellSize / 2;
                next_center_y = Spacing + Route[i + 1].X * CellSize + CellSize / 2;
                using (Graphics graphics = Graphics.FromImage(bmp))
                {
                    graphics.DrawLine(Pens.Red, center_x, center_y, next_center_x, next_center_y);
                    graphics.Dispose();
                }
            }
            using (Graphics graphics = Graphics.FromImage(bmp))
            {
                subForm.Draw_Point(graphics, Brushes.DeepSkyBlue, Route_Start_Pos, 4);
                subForm.Draw_Point(graphics, Brushes.Magenta, Route_End_Pos, 4);
                graphics.Dispose();
            }
            subForm.MazePicture.Image = bmp;
            ShowRouteAnimation.Enabled = true;
        }

        private Point Find_Smallest_F_In_Opened_Cells()
        {
            Point Smallest_F_Point = new Point(-1, -1);
            int min = int.MaxValue;
            for (int i = 0; i < Opened_Cells.Count; ++i)
            {
                if (F_Value_Of_Node[Opened_Cells[i].X, Opened_Cells[i].Y] <= min)
                {
                    F_Value_Of_Node[Opened_Cells[i].X, Opened_Cells[i].Y] = min;
                    Smallest_F_Point = Opened_Cells[i];
                }
            }
            return Smallest_F_Point;
        }

        private void Reset_Route_Info()
        {
            for (int i = 0; i < CellCnt; ++i)
            {
                for (int j = 0; j < CellCnt; ++j)
                {
                    Parent_Of_Node[i, j] = new Point(-1, -1);
                    F_Value_Of_Node[i, j] = 0;
                    G_Value_Of_Node[i, j] = 0;
                    H_Value_Of_Node[i, j] = 0;
                }
            }
            Opened_Cells.Clear();
            Closed_Cells.Clear();
            Route.Clear();
        }

        private void ShowRouteAnimation_Click(object sender, EventArgs e)
        {
            CreateMaze.Enabled = false;
            ResetMaze.Enabled = false;
            SaveMazeData.Enabled = false;
            LoadMazeData.Enabled = false;
            ResetRoutePoints.Enabled = false;
            FindRoute.Enabled = false;
            ShowRouteAnimation.Enabled = false;
            subForm.MazePicture.Refresh();
            int center_x, center_y, next_center_x, next_center_y;
            int move_x = 0, move_y = 0;
            int current_x, current_y;
            int side_len = 4;
            for (int i = 0; i < Route.Count - 1; ++i) // 경로의 처음부터 끝까지 사각형들을 그리면서 경로 진행 과정을 시각적으로 묘사
            {
                center_x = Spacing + Route[i].Y * CellSize + CellSize / 2;
                center_y = Spacing + Route[i].X * CellSize + CellSize / 2;
                next_center_x = Spacing + Route[i + 1].Y * CellSize + CellSize / 2;
                next_center_y = Spacing + Route[i + 1].X * CellSize + CellSize / 2;
                current_x = center_x;
                current_y = center_y;
                // 경로가 진행되는 방향에 맞게 사각형들이 그려지도록 움직일 방향 설정
                if (center_x < next_center_x)
                {
                    move_x = 1;
                    move_y = 0;
                }
                else if (center_x > next_center_x)
                {
                    move_x = -1;
                    move_y = 0;
                }
                else if (center_y < next_center_y)
                {
                    move_x = 0;
                    move_y = 1;
                }
                else if (center_y > next_center_y)
                {
                    move_x = 0;
                    move_y = -1;
                }
                for (int j = 0; j < CellSize; ++j)
                {
                    using (Graphics graphics = subForm.MazePicture.CreateGraphics())
                    {
                        graphics.FillRectangle(Brushes.Red, current_x - side_len/2, current_y - side_len / 2, side_len, side_len);
                        current_x += move_x;
                        current_y += move_y;
                    }
                }
                DelaySystem(10); // 경로 진행 과정이 너무 빨리 지나가지 않도록 실행 중간중간에 쉬는 구간 삽입
            }
            using (Graphics graphics = subForm.MazePicture.CreateGraphics())
            {
                subForm.Draw_Point(graphics, Brushes.DeepSkyBlue, Route_Start_Pos, 4);
                subForm.Draw_Point(graphics, Brushes.Magenta, Route_End_Pos, 4);
            }
            DelaySystem(1000);
            subForm.MazePicture.Refresh();
            CreateMaze.Enabled = true;
            ResetMaze.Enabled = true;
            SaveMazeData.Enabled = true;
            LoadMazeData.Enabled = true;
            ResetRoutePoints.Enabled = true;
            FindRoute.Enabled = true;
            ShowRouteAnimation.Enabled = true;
        }

        private void DelaySystem(int ms)
        {
            DateTime ThisMoment = DateTime.Now;
            TimeSpan Duration = new TimeSpan(0, 0, 0, 0, ms);
            DateTime AfterWards = ThisMoment.Add(Duration);
            while (AfterWards >= ThisMoment)
            {
                System.Windows.Forms.Application.DoEvents(); // 지연 시간 동안 다른 윈도우 이벤트 처리가 가능하도록 동작
                ThisMoment = DateTime.Now;
            }
        }
    }
}