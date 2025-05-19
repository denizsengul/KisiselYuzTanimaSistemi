using System;
using System.Drawing;
using System.Windows.Forms;

public class PromptDialog : Form
{
    public string InputText => txtInput.Text;
    private TextBox txtInput;

    public PromptDialog(string message)
    {
        this.Text = "Ad Soyad Girişi";
        this.Width = 350;
        this.Height = 160;
        this.StartPosition = FormStartPosition.CenterParent;
        this.FormBorderStyle = FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;
        this.MinimizeBox = false;

        Label lblMessage = new Label()
        {
            Left = 10,
            Top = 10,
            Text = message,
            Width = 320,
            Font = new Font("Segoe UI", 10, FontStyle.Bold)
        };
        this.Controls.Add(lblMessage);

        txtInput = new TextBox()
        {
            Left = 10,
            Top = 40,
            Width = 320,
            Font = new Font("Segoe UI", 10)
        };
        this.Controls.Add(txtInput);

        Button buttonOk = new Button()
        {
            Text = "Tamam",
            Left = 240,
            Width = 90,
            Top = 75,
            DialogResult = DialogResult.OK
        };
        buttonOk.Click += (sender, e) =>
        {
            if (string.IsNullOrWhiteSpace(txtInput.Text))
            {
                MessageBox.Show("Lütfen ad soyad giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        };

        this.Controls.Add(buttonOk);
        this.AcceptButton = buttonOk;
    }
}
