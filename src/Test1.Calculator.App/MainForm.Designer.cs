#nullable enable
namespace Test1.Calculator.App;

partial class MainForm
{
    private System.ComponentModel.IContainer? components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components is not null))
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        txtDisplay = new TextBox();
        tableButtons = new TableLayoutPanel();
        btnClear = new Button();
        btnDelete = new Button();
        btnDivide = new Button();
        btnDigit7 = new Button();
        btnDigit8 = new Button();
        btnDigit9 = new Button();
        btnMultiply = new Button();
        btnDigit4 = new Button();
        btnDigit5 = new Button();
        btnDigit6 = new Button();
        btnSubtract = new Button();
        btnDigit1 = new Button();
        btnDigit2 = new Button();
        btnDigit3 = new Button();
        btnAdd = new Button();
        btnDigit0 = new Button();
        btnDecimalPoint = new Button();
        btnEquals = new Button();
        tableButtons.SuspendLayout();
        SuspendLayout();
        //
        // txtDisplay
        //
        txtDisplay.Dock = DockStyle.Top;
        txtDisplay.Name = "txtDisplay";
        txtDisplay.ReadOnly = true;
        txtDisplay.TabStop = false;
        txtDisplay.Text = "0";
        txtDisplay.TextAlign = HorizontalAlignment.Right;
        //
        // tableButtons
        //
        tableButtons.ColumnCount = 4;
        tableButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
        tableButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
        tableButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
        tableButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
        tableButtons.Controls.Add(btnClear, 0, 0);
        tableButtons.Controls.Add(btnDelete, 1, 0);
        tableButtons.Controls.Add(btnDivide, 3, 0);
        tableButtons.Controls.Add(btnDigit7, 0, 1);
        tableButtons.Controls.Add(btnDigit8, 1, 1);
        tableButtons.Controls.Add(btnDigit9, 2, 1);
        tableButtons.Controls.Add(btnMultiply, 3, 1);
        tableButtons.Controls.Add(btnDigit4, 0, 2);
        tableButtons.Controls.Add(btnDigit5, 1, 2);
        tableButtons.Controls.Add(btnDigit6, 2, 2);
        tableButtons.Controls.Add(btnSubtract, 3, 2);
        tableButtons.Controls.Add(btnDigit1, 0, 3);
        tableButtons.Controls.Add(btnDigit2, 1, 3);
        tableButtons.Controls.Add(btnDigit3, 2, 3);
        tableButtons.Controls.Add(btnAdd, 3, 3);
        tableButtons.Controls.Add(btnDigit0, 0, 4);
        tableButtons.Controls.Add(btnDecimalPoint, 1, 4);
        tableButtons.Controls.Add(btnEquals, 2, 4);
        tableButtons.Dock = DockStyle.Fill;
        tableButtons.Name = "tableButtons";
        tableButtons.RowCount = 5;
        tableButtons.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
        tableButtons.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
        tableButtons.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
        tableButtons.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
        tableButtons.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
        //
        // btnClear
        //
        btnClear.Dock = DockStyle.Fill;
        btnClear.Name = "btnClear";
        btnClear.Text = "Clear";
        btnClear.UseVisualStyleBackColor = true;
        btnClear.Click += btnClear_Click;
        //
        // btnDelete
        //
        btnDelete.Dock = DockStyle.Fill;
        btnDelete.Name = "btnDelete";
        btnDelete.Text = "Delete";
        btnDelete.UseVisualStyleBackColor = true;
        btnDelete.Click += btnDelete_Click;
        //
        // btnDivide
        //
        btnDivide.Dock = DockStyle.Fill;
        btnDivide.Name = "btnDivide";
        btnDivide.Text = "/";
        btnDivide.UseVisualStyleBackColor = true;
        btnDivide.Click += btnDivide_Click;
        //
        // btnDigit7
        //
        btnDigit7.Dock = DockStyle.Fill;
        btnDigit7.Name = "btnDigit7";
        btnDigit7.Tag = 7;
        btnDigit7.Text = "7";
        btnDigit7.UseVisualStyleBackColor = true;
        btnDigit7.Click += DigitButton_Click;
        //
        // btnDigit8
        //
        btnDigit8.Dock = DockStyle.Fill;
        btnDigit8.Name = "btnDigit8";
        btnDigit8.Tag = 8;
        btnDigit8.Text = "8";
        btnDigit8.UseVisualStyleBackColor = true;
        btnDigit8.Click += DigitButton_Click;
        //
        // btnDigit9
        //
        btnDigit9.Dock = DockStyle.Fill;
        btnDigit9.Name = "btnDigit9";
        btnDigit9.Tag = 9;
        btnDigit9.Text = "9";
        btnDigit9.UseVisualStyleBackColor = true;
        btnDigit9.Click += DigitButton_Click;
        //
        // btnMultiply
        //
        btnMultiply.Dock = DockStyle.Fill;
        btnMultiply.Name = "btnMultiply";
        btnMultiply.Text = "*";
        btnMultiply.UseVisualStyleBackColor = true;
        btnMultiply.Click += btnMultiply_Click;
        //
        // btnDigit4
        //
        btnDigit4.Dock = DockStyle.Fill;
        btnDigit4.Name = "btnDigit4";
        btnDigit4.Tag = 4;
        btnDigit4.Text = "4";
        btnDigit4.UseVisualStyleBackColor = true;
        btnDigit4.Click += DigitButton_Click;
        //
        // btnDigit5
        //
        btnDigit5.Dock = DockStyle.Fill;
        btnDigit5.Name = "btnDigit5";
        btnDigit5.Tag = 5;
        btnDigit5.Text = "5";
        btnDigit5.UseVisualStyleBackColor = true;
        btnDigit5.Click += DigitButton_Click;
        //
        // btnDigit6
        //
        btnDigit6.Dock = DockStyle.Fill;
        btnDigit6.Name = "btnDigit6";
        btnDigit6.Tag = 6;
        btnDigit6.Text = "6";
        btnDigit6.UseVisualStyleBackColor = true;
        btnDigit6.Click += DigitButton_Click;
        //
        // btnSubtract
        //
        btnSubtract.Dock = DockStyle.Fill;
        btnSubtract.Name = "btnSubtract";
        btnSubtract.Text = "-";
        btnSubtract.UseVisualStyleBackColor = true;
        btnSubtract.Click += btnSubtract_Click;
        //
        // btnDigit1
        //
        btnDigit1.Dock = DockStyle.Fill;
        btnDigit1.Name = "btnDigit1";
        btnDigit1.Tag = 1;
        btnDigit1.Text = "1";
        btnDigit1.UseVisualStyleBackColor = true;
        btnDigit1.Click += DigitButton_Click;
        //
        // btnDigit2
        //
        btnDigit2.Dock = DockStyle.Fill;
        btnDigit2.Name = "btnDigit2";
        btnDigit2.Tag = 2;
        btnDigit2.Text = "2";
        btnDigit2.UseVisualStyleBackColor = true;
        btnDigit2.Click += DigitButton_Click;
        //
        // btnDigit3
        //
        btnDigit3.Dock = DockStyle.Fill;
        btnDigit3.Name = "btnDigit3";
        btnDigit3.Tag = 3;
        btnDigit3.Text = "3";
        btnDigit3.UseVisualStyleBackColor = true;
        btnDigit3.Click += DigitButton_Click;
        //
        // btnAdd
        //
        btnAdd.Dock = DockStyle.Fill;
        btnAdd.Name = "btnAdd";
        btnAdd.Text = "+";
        btnAdd.UseVisualStyleBackColor = true;
        btnAdd.Click += btnAdd_Click;
        //
        // btnDigit0
        //
        btnDigit0.Dock = DockStyle.Fill;
        btnDigit0.Name = "btnDigit0";
        btnDigit0.Tag = 0;
        btnDigit0.Text = "0";
        btnDigit0.UseVisualStyleBackColor = true;
        btnDigit0.Click += DigitButton_Click;
        //
        // btnDecimalPoint
        //
        btnDecimalPoint.Dock = DockStyle.Fill;
        btnDecimalPoint.Name = "btnDecimalPoint";
        btnDecimalPoint.Text = ".";
        btnDecimalPoint.UseVisualStyleBackColor = true;
        btnDecimalPoint.Click += btnDecimalPoint_Click;
        //
        // btnEquals
        //
        btnEquals.Dock = DockStyle.Fill;
        btnEquals.Name = "btnEquals";
        btnEquals.Text = "=";
        btnEquals.UseVisualStyleBackColor = true;
        btnEquals.Click += btnEquals_Click;
        //
        // MainForm
        //
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(320, 420);
        Controls.Add(tableButtons);
        Controls.Add(txtDisplay);
        MinimumSize = new Size(320, 420);
        Name = "MainForm";
        Text = "Calculator";
        tableButtons.ResumeLayout(false);
        ResumeLayout(false);
        PerformLayout();
    }

    private TextBox txtDisplay = new();
    private TableLayoutPanel tableButtons = new();
    private Button btnDigit0 = new();
    private Button btnDigit1 = new();
    private Button btnDigit2 = new();
    private Button btnDigit3 = new();
    private Button btnDigit4 = new();
    private Button btnDigit5 = new();
    private Button btnDigit6 = new();
    private Button btnDigit7 = new();
    private Button btnDigit8 = new();
    private Button btnDigit9 = new();
    private Button btnDecimalPoint = new();
    private Button btnAdd = new();
    private Button btnSubtract = new();
    private Button btnMultiply = new();
    private Button btnDivide = new();
    private Button btnEquals = new();
    private Button btnClear = new();
    private Button btnDelete = new();
}