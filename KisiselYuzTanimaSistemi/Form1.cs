using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Drawing;
using System.IO;
using System.Media;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace UykuluSurucuTespitSistemi
{
    public partial class Form1 : Form
    {
        private VideoCapture capture;
        private CascadeClassifier faceCascade;
        private CascadeClassifier eyeCascade;
        private Thread cameraThread;
        private bool isRunning = false;
        private int closedEyeSeconds = 0;
        private bool isDarkMode = false;

        private Button btnExport;
        private Button btnTheme;

        public Form1()
        {
            InitializeComponent();
            InitUI();
            InitDataGridView();
            AddExportButton();
            AddThemeToggleButton();
            StyleControlButtons();
            timer1.Interval = 1000;
            timer1.Start();
            this.Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ArrangeBottomButtons();

            string savedNamePath = "user_name.txt";
            if (File.Exists(savedNamePath))
            {
                string savedName = File.ReadAllText(savedNamePath);
                var hosLabel = this.Controls.Find("lblHosGeldin", true);
                if (hosLabel.Length > 0)
                {
                    hosLabel[0].Text = "Hoş geldin, " + savedName;
                    hosLabel[0].Visible = true;
                    hosLabel[0].BringToFront();
                }
            }
        }
        

        private void InitUI()
        {
            // Kullanıcı adı karşılama etiketi
            if (!this.Controls.ContainsKey("lblHosGeldin"))
            {
                Label lblHosGeldin = new Label
                {
                    Name = "lblHosGeldin",
                    AutoSize = true,
                    Location = new System.Drawing.Point(15, 0),
                    Font = new Font("Segoe UI", 12, FontStyle.Bold),
                    ForeColor = Color.DarkBlue
                };
                this.Controls.Add(lblHosGeldin);
            }
            this.Text = "Uykulu Sürücü Tespit Sistemi";
            this.BackColor = Color.White;
            this.Font = new Font("Segoe UI", 10, FontStyle.Regular);

            lblGozDurumu.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            lblDurum.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            lblDurum.ForeColor = Color.DarkGreen;

            lblSaat.BackColor = Color.Black;
            lblSaat.ForeColor = Color.White;
            lblSaat.Font = new Font("Consolas", 10, FontStyle.Bold);
            lblSaat.AutoSize = true;
            lblSaat.BringToFront();
            pictureBox1.Controls.Add(lblSaat);
            lblSaat.Location = new System.Drawing.Point(5, 5);

            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void StyleControlButtons()
        {
            btnStart.BackColor = Color.MediumSeaGreen;
            btnStart.ForeColor = Color.White;
            btnStart.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnStart.FlatStyle = FlatStyle.Flat;
            btnStart.FlatAppearance.BorderSize = 0;

            btnStop.BackColor = Color.IndianRed;
            btnStop.ForeColor = Color.White;
            btnStop.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnStop.FlatStyle = FlatStyle.Flat;
            btnStop.FlatAppearance.BorderSize = 0;
        }

        private void InitDataGridView()
        {
            dataGridView1.Columns.Clear();
            dataGridView1.AutoGenerateColumns = false;

            DataGridViewTextBoxColumn col1 = new DataGridViewTextBoxColumn
            {
                HeaderText = "Kullanıcı",
                Name = "Kullanici",
                Width = 200
            };

            DataGridViewTextBoxColumn col2 = new DataGridViewTextBoxColumn
            {
                HeaderText = "Zaman",
                Name = "Zaman",
                Width = 250
            };

            dataGridView1.Columns.Add(col1);
            dataGridView1.Columns.Add(col2);
        }

        private void AddExportButton()
        {
            btnExport = new Button
            {
                Text = "Excel'e Aktar",
                Size = new System.Drawing.Size(130, 40),
                BackColor = Color.SteelBlue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            btnExport.FlatAppearance.BorderSize = 0;
            btnExport.Click += ExportToExcel;
            this.Controls.Add(btnExport);
        }

        private void AddThemeToggleButton()
        {
            btnTheme = new Button
            {
                Text = "Tema Değiştir",
                Size = new System.Drawing.Size(130, 40),
                BackColor = Color.DarkOrange,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            btnTheme.FlatAppearance.BorderSize = 0;
            btnTheme.Click += ToggleTheme;
            this.Controls.Add(btnTheme);
        }

        private void ArrangeBottomButtons()
        {
            if (btnExport == null || btnTheme == null || btnStart == null) return;

            int spacing = 20;
            int startX = btnStart.Left;
            int y = btnStart.Bottom + 210; // Önceden 20 idi, şimdi 150 yaptık (yaklaşık 30 cm)

            btnExport.Location = new System.Drawing.Point(startX, y);
            btnTheme.Location = new System.Drawing.Point(startX + btnExport.Width + spacing, y);

            btnExport.Visible = true;
            btnTheme.Visible = true;
            btnExport.BringToFront();
            btnTheme.BringToFront();
        }

        private void ExportToExcel(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Excel Files|*.csv",
                Title = "Verileri Dışa Aktar",
                FileName = "surucu_kayitlari.csv"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                StringBuilder sb = new StringBuilder();
                foreach (DataGridViewColumn col in dataGridView1.Columns)
                    sb.Append(col.HeaderText + ",");

                sb.AppendLine();

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                        sb.Append(cell.Value?.ToString() + ",");
                    sb.AppendLine();
                }

                File.WriteAllText(saveFileDialog.FileName, sb.ToString(), Encoding.UTF8);
                MessageBox.Show("CSV başarıyla kaydedildi.");
            }
        }

        private void ToggleTheme(object sender, EventArgs e)
        {
            isDarkMode = !isDarkMode;
            Color bg = isDarkMode ? Color.FromArgb(30, 30, 30) : Color.WhiteSmoke;
            Color fg = isDarkMode ? Color.White : Color.Black;

            this.BackColor = bg;
            foreach (Control ctrl in this.Controls)
            {
                ctrl.BackColor = bg;
                ctrl.ForeColor = fg;
            }

            dataGridView1.BackgroundColor = bg;
            dataGridView1.DefaultCellStyle.BackColor = bg;
            dataGridView1.DefaultCellStyle.ForeColor = fg;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            string savedNamePath = "user_name.txt";
            string savedName = File.Exists(savedNamePath) ? File.ReadAllText(savedNamePath) : string.Empty;

            if (!string.IsNullOrEmpty(savedName))
            {
                var hosLabel = this.Controls.Find("lblHosGeldin", true);
                if (hosLabel.Length > 0)
                {
                    hosLabel[0].Text = "Hoş geldin, " + savedName;
                    hosLabel[0].Visible = true;
                    hosLabel[0].BringToFront();
                }
            }
            else
            {
                using (Form prompt = new Form())
                {
                    prompt.Width = 300;
                    prompt.Height = 180;
                    prompt.Text = "Kullanıcı Kaydı";

                    Label lbl = new Label() { Left = 20, Top = 20, Text = "Ad Soyad" };
                    TextBox inputBox = new TextBox() { Left = 20, Top = 50, Width = 240 };
                    Button confirm = new Button() { Text = "Kaydet", Left = 100, Width = 80, Top = 90 };
                    confirm.Click += (s, ev) => {
                        File.WriteAllText(savedNamePath, inputBox.Text);
                        var hosLabel = this.Controls.Find("lblHosGeldin", true);
                        if (hosLabel.Length > 0)
                        {
                            hosLabel[0].Text = "Hoş geldin, " + inputBox.Text;
                            hosLabel[0].Visible = true;
                            hosLabel[0].BringToFront();
                        }
                        prompt.Close();
                    };
                    prompt.Controls.Add(lbl);
                    prompt.Controls.Add(inputBox);
                    prompt.Controls.Add(confirm);
                    prompt.ShowDialog();
                }
            }
            if (isRunning) return;

            capture = new VideoCapture(0);
            if (!capture.IsOpened())
            {
                MessageBox.Show("Kamera açılamadı.");
                return;
            }

            faceCascade = new CascadeClassifier("haarcascade_frontalface_default.xml");
            eyeCascade = new CascadeClassifier("haarcascade_eye.xml");

            isRunning = true;
            cameraThread = new Thread(ProcessCamera);
            cameraThread.Start();
            lblDurum.Text = "Durum: Başlatıldı.";
            lblDurum.ForeColor = Color.DarkGreen;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            isRunning = false;
            cameraThread?.Join();
            capture?.Release();
            pictureBox1.Image = null;
            lblDurum.Text = "Durum: Durduruldu.";
            lblDurum.ForeColor = Color.DarkRed;
        }

        private void ProcessCamera()
        {
            while (isRunning)
            {
                using (Mat frame = new Mat())
                {
                    capture.Read(frame);
                    if (frame.Empty()) continue;

                    var gray = new Mat();
                    Cv2.CvtColor(frame, gray, ColorConversionCodes.BGR2GRAY);
                    var faces = faceCascade.DetectMultiScale(gray, 1.1, 4);

                    foreach (var face in faces)
                    {
                        Cv2.Rectangle(frame, face, Scalar.Red, 2);
                        var faceROI = new Mat(gray, face);
                        var eyes = eyeCascade.DetectMultiScale(faceROI, 1.1, 4);

                        if (eyes.Length > 0)
                        {
                            closedEyeSeconds = 0;
                            SetLabelText(lblGozDurumu, "Gözler açık", Color.Green);
                        }
                        else
                        {
                            closedEyeSeconds++;
                            SetLabelText(lblGozDurumu, "Gözler kapalı", Color.Red);
                            SetLabelText(lblKapaliSure, $"Göz kapalı süresi: {closedEyeSeconds} sn", Color.Black);

                            if (closedEyeSeconds >= 3)
                            {
                                SystemSounds.Beep.Play();
                                AddToTable("Yakup Atalar", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                                closedEyeSeconds = 0;
                            }
                        }
                    }

                    var bitmap = BitmapConverter.ToBitmap(frame);
                    pictureBox1.Invoke(new Action(() => pictureBox1.Image = bitmap));
                }

                Thread.Sleep(100);
            }
        }

        private void AddToTable(string name, string time)
        {
            if (dataGridView1.InvokeRequired)
                dataGridView1.Invoke(new Action(() => dataGridView1.Rows.Add(name, time)));
            else
                dataGridView1.Rows.Add(name, time);
        }

        private void SetLabelText(Label label, string text, Color color)
        {
            if (label.InvokeRequired)
            {
                label.Invoke(new Action(() => {
                    label.Text = text;
                    label.ForeColor = color;
                }));
            }
            else
            {
                label.Text = text;
                label.ForeColor = color;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblSaat.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
