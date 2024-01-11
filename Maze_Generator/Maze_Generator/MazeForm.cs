using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Maze_Generator
{
    public partial class MazeForm : Form
    {
        private ControlForm mainForm;
        public MazeForm()
        {
            InitializeComponent();
        }

        public MazeForm(ControlForm frm)
        {
            InitializeComponent();
            mainForm = frm;
        }

        private void MazePicture_MouseClick(object sender, MouseEventArgs e)
        {
            if (!mainForm.Is_Maze_Ready) // 미로가 생성되거나 불려온 상태가 아니라면 출발점과 도착점 지정 금지
            {
                return;
            }
            // 선택한 위치가 미로의 유효한 범위 밖이라면 출발점과 도착점 지정 금지
            if (e.X < mainForm.Spacing || e.Y < mainForm.Spacing ||
                e.X >= mainForm.Spacing + mainForm.CellCnt * mainForm.CellSize ||
                e.Y >= mainForm.Spacing + mainForm.CellCnt * mainForm.CellSize)
            {
                return;
            }
            Bitmap bmp = new Bitmap(MazePicture.ClientSize.Width, MazePicture.ClientSize.Height);
            bmp = mainForm.Create_Refresh_Image(bmp);
            // 도착점이 그려진 위치에 출발점이 덮어씌워지지 않도록 지정
            if (e.Button == MouseButtons.Left && Set_Position(e.Location) != mainForm.Route_End_Pos)
            {
                // 출발점으로 선택한 위치가 기존의 출발점과 같다면 화면 갱신 불필요
                if (Set_Position(e.Location) == mainForm.Route_Start_Pos)
                {
                    return;
                }
                // 출발점의 위치가 기존의 출발점과 다르다면 경로를 다시 찾아야하므로 경로 애니메이션 기능 비활성화
                else
                {
                    mainForm.ShowRouteAnimation.Enabled = false;
                }
                mainForm.Route_Start_Pos = Set_Position(e.Location);
                using (Graphics graphics = Graphics.FromImage(bmp))
                {
                    Draw_Point(graphics, Brushes.DeepSkyBlue, mainForm.Route_Start_Pos, 4);
                    if (mainForm.Route_End_Pos != new Point(-1, -1)) // 도착점이 이미 그려진 상태라면 도착점이 없어지지 않게 표시
                    {
                        Draw_Point(graphics, Brushes.Magenta, mainForm.Route_End_Pos, 4);
                    }
                    if (MazePicture.Image != null)
                    {
                        MazePicture.Image.Dispose();
                        MazePicture.Image = null;
                    }
                    MazePicture.Image = bmp;
                    graphics.Dispose();
                }
            }
            // 출발점이 그려진 위치에 도착점이 덮어씌워지지 않도록 지정
            else if (e.Button == MouseButtons.Right && Set_Position(e.Location) != mainForm.Route_Start_Pos)
            {
                // 도착점으로 선택한 위치가 기존의 도착점과 같다면 화면 갱신 불필요
                if (Set_Position(e.Location) == mainForm.Route_End_Pos)
                {
                    return;
                }
                // 도착점의 위치가 기존의 도착점과 다르다면 경로를 다시 찾아야하므로 경로 애니메이션 기능 비활성화
                else
                {
                    mainForm.ShowRouteAnimation.Enabled = false;
                }
                mainForm.Route_End_Pos = Set_Position(e.Location);
                using (Graphics graphics = Graphics.FromImage(bmp))
                {
                    Draw_Point(graphics, Brushes.Magenta, mainForm.Route_End_Pos, 4);
                    if (mainForm.Route_Start_Pos != new Point(-1, -1)) // 출발점이 이미 그려진 상태라면 출발점이 없어지지 않게 표시
                    {
                        Draw_Point(graphics, Brushes.DeepSkyBlue, mainForm.Route_Start_Pos, 4);
                    }
                    if (MazePicture.Image != null)
                    {
                        MazePicture.Image.Dispose();
                        MazePicture.Image = null;
                    }
                    MazePicture.Image = bmp;
                    graphics.Dispose();
                }
            }
            // 출발점과 도착점이 지정되었다면 출발점 & 도착점 초기화 기능과 경로 탐색 기능 활성화
            if (mainForm.Route_Start_Pos != new Point(-1, -1) && mainForm.Route_End_Pos != new Point(-1, -1))
            {
                mainForm.ResetRoutePoints.Enabled = true;
                mainForm.FindRoute.Enabled = true;
            }
            MazePicture.Refresh();
        }

        private Point Set_Position(Point point)
        {
            int row = (point.Y - mainForm.Spacing) / mainForm.CellSize;
            int col = (point.X - mainForm.Spacing) / mainForm.CellSize;
            return new Point(row, col);
        }

        public void Draw_Point(Graphics graphics, Brush brush, Point pos, int radius)
        {
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            int center_x = mainForm.Spacing + pos.Y * mainForm.CellSize + mainForm.CellSize / 2;
            int center_y = mainForm.Spacing + pos.X * mainForm.CellSize + mainForm.CellSize / 2;
            graphics.FillEllipse(brush, center_x - radius, center_y - radius, radius * 2, radius * 2);
        }
    }
}
