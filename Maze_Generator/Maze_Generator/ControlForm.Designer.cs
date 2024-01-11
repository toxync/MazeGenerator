namespace Maze_Generator
{
    partial class ControlForm
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.CreateMaze = new System.Windows.Forms.Button();
            this.SaveMazeData = new System.Windows.Forms.Button();
            this.LoadMazeData = new System.Windows.Forms.Button();
            this.ResetMaze = new System.Windows.Forms.Button();
            this.AddMazeToList = new System.Windows.Forms.Button();
            this.DeleteMazeFromList = new System.Windows.Forms.Button();
            this.MazeList = new System.Windows.Forms.ListBox();
            this.MazeListLabel = new System.Windows.Forms.Label();
            this.MazePathList = new System.Windows.Forms.ListBox();
            this.InstructionLabel = new System.Windows.Forms.Label();
            this.FindRoute = new System.Windows.Forms.Button();
            this.ResetRoutePoints = new System.Windows.Forms.Button();
            this.MazePathListLabel = new System.Windows.Forms.Label();
            this.ShowRouteAnimation = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // CreateMaze
            // 
            this.CreateMaze.Location = new System.Drawing.Point(862, 30);
            this.CreateMaze.Name = "CreateMaze";
            this.CreateMaze.Size = new System.Drawing.Size(199, 46);
            this.CreateMaze.TabIndex = 0;
            this.CreateMaze.Text = "미로 생성";
            this.CreateMaze.UseVisualStyleBackColor = true;
            this.CreateMaze.Click += new System.EventHandler(this.CreateMaze_Click);
            // 
            // SaveMazeData
            // 
            this.SaveMazeData.Enabled = false;
            this.SaveMazeData.Location = new System.Drawing.Point(862, 166);
            this.SaveMazeData.Name = "SaveMazeData";
            this.SaveMazeData.Size = new System.Drawing.Size(199, 46);
            this.SaveMazeData.TabIndex = 1;
            this.SaveMazeData.Text = "미로 데이터 저장";
            this.SaveMazeData.UseVisualStyleBackColor = true;
            this.SaveMazeData.Click += new System.EventHandler(this.SaveMazeData_Click);
            // 
            // LoadMazeData
            // 
            this.LoadMazeData.Enabled = false;
            this.LoadMazeData.Location = new System.Drawing.Point(862, 234);
            this.LoadMazeData.Name = "LoadMazeData";
            this.LoadMazeData.Size = new System.Drawing.Size(199, 46);
            this.LoadMazeData.TabIndex = 2;
            this.LoadMazeData.Text = "미로 데이터 불러오기";
            this.LoadMazeData.UseVisualStyleBackColor = true;
            this.LoadMazeData.Click += new System.EventHandler(this.LoadMazeData_Click);
            // 
            // ResetMaze
            // 
            this.ResetMaze.Enabled = false;
            this.ResetMaze.Location = new System.Drawing.Point(862, 98);
            this.ResetMaze.Name = "ResetMaze";
            this.ResetMaze.Size = new System.Drawing.Size(199, 46);
            this.ResetMaze.TabIndex = 3;
            this.ResetMaze.Text = "미로 초기화";
            this.ResetMaze.UseVisualStyleBackColor = true;
            this.ResetMaze.Click += new System.EventHandler(this.ResetMaze_Click);
            // 
            // AddMazeToList
            // 
            this.AddMazeToList.Location = new System.Drawing.Point(30, 30);
            this.AddMazeToList.Name = "AddMazeToList";
            this.AddMazeToList.Size = new System.Drawing.Size(199, 46);
            this.AddMazeToList.TabIndex = 4;
            this.AddMazeToList.Text = "목록에 미로 추가";
            this.AddMazeToList.UseVisualStyleBackColor = true;
            this.AddMazeToList.Click += new System.EventHandler(this.AddMazeToList_Click);
            // 
            // DeleteMazeFromList
            // 
            this.DeleteMazeFromList.Enabled = false;
            this.DeleteMazeFromList.Location = new System.Drawing.Point(271, 30);
            this.DeleteMazeFromList.Name = "DeleteMazeFromList";
            this.DeleteMazeFromList.Size = new System.Drawing.Size(199, 46);
            this.DeleteMazeFromList.TabIndex = 5;
            this.DeleteMazeFromList.Text = "목록에서 미로 삭제";
            this.DeleteMazeFromList.UseVisualStyleBackColor = true;
            this.DeleteMazeFromList.Click += new System.EventHandler(this.DeleteMazeFromList_Click);
            // 
            // MazeList
            // 
            this.MazeList.FormattingEnabled = true;
            this.MazeList.ItemHeight = 18;
            this.MazeList.Location = new System.Drawing.Point(30, 125);
            this.MazeList.Name = "MazeList";
            this.MazeList.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.MazeList.Size = new System.Drawing.Size(350, 346);
            this.MazeList.TabIndex = 6;
            this.MazeList.SelectedIndexChanged += new System.EventHandler(this.MazeList_SelectedIndexChanged);
            // 
            // MazeListLabel
            // 
            this.MazeListLabel.AutoSize = true;
            this.MazeListLabel.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.MazeListLabel.Location = new System.Drawing.Point(27, 99);
            this.MazeListLabel.Name = "MazeListLabel";
            this.MazeListLabel.Size = new System.Drawing.Size(86, 18);
            this.MazeListLabel.TabIndex = 7;
            this.MazeListLabel.Text = "미로 목록";
            // 
            // MazePathList
            // 
            this.MazePathList.Enabled = false;
            this.MazePathList.FormattingEnabled = true;
            this.MazePathList.HorizontalScrollbar = true;
            this.MazePathList.ItemHeight = 18;
            this.MazePathList.Location = new System.Drawing.Point(427, 125);
            this.MazePathList.Name = "MazePathList";
            this.MazePathList.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.MazePathList.Size = new System.Drawing.Size(350, 346);
            this.MazePathList.TabIndex = 8;
            // 
            // InstructionLabel
            // 
            this.InstructionLabel.AutoSize = true;
            this.InstructionLabel.Location = new System.Drawing.Point(34, 496);
            this.InstructionLabel.Name = "InstructionLabel";
            this.InstructionLabel.Size = new System.Drawing.Size(626, 36);
            this.InstructionLabel.TabIndex = 9;
            this.InstructionLabel.Text = "미로의 생성이나 불러오기가 완료되면 마우스 좌클릭으로 출발점(청록색)을, \r\n마우스 우클릭으로 도착점(연보라색)을 설정한 뒤 경로 탐색 버튼을 누" +
    "르시오";
            // 
            // FindRoute
            // 
            this.FindRoute.Enabled = false;
            this.FindRoute.Location = new System.Drawing.Point(862, 370);
            this.FindRoute.Name = "FindRoute";
            this.FindRoute.Size = new System.Drawing.Size(199, 46);
            this.FindRoute.TabIndex = 10;
            this.FindRoute.Text = "경로 탐색";
            this.FindRoute.UseVisualStyleBackColor = true;
            this.FindRoute.Click += new System.EventHandler(this.FindRoute_Click);
            // 
            // ResetRoutePoints
            // 
            this.ResetRoutePoints.Enabled = false;
            this.ResetRoutePoints.Location = new System.Drawing.Point(862, 302);
            this.ResetRoutePoints.Name = "ResetRoutePoints";
            this.ResetRoutePoints.Size = new System.Drawing.Size(199, 46);
            this.ResetRoutePoints.TabIndex = 11;
            this.ResetRoutePoints.Text = "출발점&&도착점 초기화";
            this.ResetRoutePoints.UseVisualStyleBackColor = true;
            this.ResetRoutePoints.Click += new System.EventHandler(this.ResetRoutePoints_Click);
            // 
            // MazePathListLabel
            // 
            this.MazePathListLabel.AutoSize = true;
            this.MazePathListLabel.Location = new System.Drawing.Point(424, 99);
            this.MazePathListLabel.Name = "MazePathListLabel";
            this.MazePathListLabel.Size = new System.Drawing.Size(230, 18);
            this.MazePathListLabel.TabIndex = 12;
            this.MazePathListLabel.Text = "각 미로 데이터의 절대 경로";
            // 
            // ShowRouteAnimation
            // 
            this.ShowRouteAnimation.Enabled = false;
            this.ShowRouteAnimation.Location = new System.Drawing.Point(862, 438);
            this.ShowRouteAnimation.Name = "ShowRouteAnimation";
            this.ShowRouteAnimation.Size = new System.Drawing.Size(199, 46);
            this.ShowRouteAnimation.TabIndex = 13;
            this.ShowRouteAnimation.Text = "경로 애니메이션 재생";
            this.ShowRouteAnimation.UseVisualStyleBackColor = true;
            this.ShowRouteAnimation.Click += new System.EventHandler(this.ShowRouteAnimation_Click);
            // 
            // ControlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1073, 582);
            this.Controls.Add(this.ShowRouteAnimation);
            this.Controls.Add(this.MazePathListLabel);
            this.Controls.Add(this.ResetRoutePoints);
            this.Controls.Add(this.FindRoute);
            this.Controls.Add(this.InstructionLabel);
            this.Controls.Add(this.MazePathList);
            this.Controls.Add(this.MazeListLabel);
            this.Controls.Add(this.MazeList);
            this.Controls.Add(this.DeleteMazeFromList);
            this.Controls.Add(this.AddMazeToList);
            this.Controls.Add(this.ResetMaze);
            this.Controls.Add(this.LoadMazeData);
            this.Controls.Add(this.SaveMazeData);
            this.Controls.Add(this.CreateMaze);
            this.Name = "ControlForm";
            this.Text = "ControlForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button CreateMaze;
        private System.Windows.Forms.Button SaveMazeData;
        private System.Windows.Forms.Button LoadMazeData;
        private System.Windows.Forms.Button ResetMaze;
        private System.Windows.Forms.Button AddMazeToList;
        private System.Windows.Forms.Button DeleteMazeFromList;
        private System.Windows.Forms.ListBox MazeList;
        private System.Windows.Forms.Label MazeListLabel;
        private System.Windows.Forms.ListBox MazePathList;
        private System.Windows.Forms.Label InstructionLabel;
        public System.Windows.Forms.Button FindRoute;
        public System.Windows.Forms.Button ResetRoutePoints;
        private System.Windows.Forms.Label MazePathListLabel;
        public System.Windows.Forms.Button ShowRouteAnimation;
    }
}

