namespace axon_console
{
    partial class AxonForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label_talcott = new System.Windows.Forms.LinkLabel();
            this.helloWorldLabel = new System.Windows.Forms.Label();
            this.textBox_filename_random = new System.Windows.Forms.TabControl();
            this.Run = new System.Windows.Forms.TabPage();
            this.button_run_async = new System.Windows.Forms.Button();
            this.button_openoutputdir = new System.Windows.Forms.Button();
            this.button_openfile = new System.Windows.Forms.Button();
            this.button_openinputdir = new System.Windows.Forms.Button();
            this.textBox_iterations = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.checkBox_multiple = new System.Windows.Forms.CheckBox();
            this.radioButton_chisquare = new System.Windows.Forms.RadioButton();
            this.radioButton_bayesian = new System.Windows.Forms.RadioButton();
            this.textBox_ThresHoldCriterium = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label_result = new System.Windows.Forms.Label();
            this.graph_linkage = new System.Windows.Forms.PictureBox();
            this.result_info = new System.Windows.Forms.TextBox();
            this.file_info = new System.Windows.Forms.TextBox();
            this.button_run = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_filename = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_outputdir = new System.Windows.Forms.TextBox();
            this.textBox_inputdir = new System.Windows.Forms.TextBox();
            this.debugInstructionsLabel = new System.Windows.Forms.Label();
            this.Test = new System.Windows.Forms.TabPage();
            this.textBox_N = new System.Windows.Forms.TextBox();
            this.labelN = new System.Windows.Forms.Label();
            this.labelVn = new System.Windows.Forms.Label();
            this.labelCn = new System.Windows.Forms.Label();
            this.textBox_Cn = new System.Windows.Forms.TextBox();
            this.textBox_Vn = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox_outputdir_random = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_filename_random2 = new System.Windows.Forms.TextBox();
            this.button_random = new System.Windows.Forms.Button();
            this.checkBox_CreateGraph = new System.Windows.Forms.CheckBox();
            this.textBox_filename_random.SuspendLayout();
            this.Run.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.graph_linkage)).BeginInit();
            this.Test.SuspendLayout();
            this.SuspendLayout();
            // 
            // label_talcott
            // 
            this.label_talcott.AutoSize = true;
            this.label_talcott.Location = new System.Drawing.Point(2602, 40);
            this.label_talcott.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label_talcott.Name = "label_talcott";
            this.label_talcott.Size = new System.Drawing.Size(111, 29);
            this.label_talcott.TabIndex = 0;
            this.label_talcott.TabStop = true;
            this.label_talcott.Text = "Talcott.nl";
            this.label_talcott.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // helloWorldLabel
            // 
            this.helloWorldLabel.AutoSize = true;
            this.helloWorldLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.helloWorldLabel.Location = new System.Drawing.Point(35, 20);
            this.helloWorldLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.helloWorldLabel.Name = "helloWorldLabel";
            this.helloWorldLabel.Size = new System.Drawing.Size(281, 55);
            this.helloWorldLabel.TabIndex = 3;
            this.helloWorldLabel.Text = "Axon server";
            // 
            // textBox_filename_random
            // 
            this.textBox_filename_random.Controls.Add(this.Run);
            this.textBox_filename_random.Controls.Add(this.Test);
            this.textBox_filename_random.Location = new System.Drawing.Point(37, 74);
            this.textBox_filename_random.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.textBox_filename_random.Name = "textBox_filename_random";
            this.textBox_filename_random.SelectedIndex = 0;
            this.textBox_filename_random.Size = new System.Drawing.Size(2683, 1321);
            this.textBox_filename_random.TabIndex = 12;
            // 
            // Run
            // 
            this.Run.BackColor = System.Drawing.Color.Gainsboro;
            this.Run.Controls.Add(this.checkBox_CreateGraph);
            this.Run.Controls.Add(this.button_run_async);
            this.Run.Controls.Add(this.button_openoutputdir);
            this.Run.Controls.Add(this.button_openfile);
            this.Run.Controls.Add(this.button_openinputdir);
            this.Run.Controls.Add(this.textBox_iterations);
            this.Run.Controls.Add(this.label7);
            this.Run.Controls.Add(this.checkBox_multiple);
            this.Run.Controls.Add(this.radioButton_chisquare);
            this.Run.Controls.Add(this.radioButton_bayesian);
            this.Run.Controls.Add(this.textBox_ThresHoldCriterium);
            this.Run.Controls.Add(this.label6);
            this.Run.Controls.Add(this.label_result);
            this.Run.Controls.Add(this.graph_linkage);
            this.Run.Controls.Add(this.result_info);
            this.Run.Controls.Add(this.file_info);
            this.Run.Controls.Add(this.button_run);
            this.Run.Controls.Add(this.label4);
            this.Run.Controls.Add(this.textBox_filename);
            this.Run.Controls.Add(this.label2);
            this.Run.Controls.Add(this.label1);
            this.Run.Controls.Add(this.textBox_outputdir);
            this.Run.Controls.Add(this.textBox_inputdir);
            this.Run.Controls.Add(this.debugInstructionsLabel);
            this.Run.Location = new System.Drawing.Point(10, 47);
            this.Run.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Run.Name = "Run";
            this.Run.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Run.Size = new System.Drawing.Size(2663, 1264);
            this.Run.TabIndex = 0;
            this.Run.Text = "Run";
            this.Run.Click += new System.EventHandler(this.Run_Click);
            // 
            // button_run_async
            // 
            this.button_run_async.Location = new System.Drawing.Point(852, 406);
            this.button_run_async.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.button_run_async.Name = "button_run_async";
            this.button_run_async.Size = new System.Drawing.Size(385, 49);
            this.button_run_async.TabIndex = 44;
            this.button_run_async.Text = "Start Axon Server Async";
            this.button_run_async.UseVisualStyleBackColor = true;
            this.button_run_async.Click += new System.EventHandler(this.button_run_async_Click);
            // 
            // button_openoutputdir
            // 
            this.button_openoutputdir.Location = new System.Drawing.Point(1248, 344);
            this.button_openoutputdir.Margin = new System.Windows.Forms.Padding(7, 7, 7, 7);
            this.button_openoutputdir.Name = "button_openoutputdir";
            this.button_openoutputdir.Size = new System.Drawing.Size(47, 45);
            this.button_openoutputdir.TabIndex = 43;
            this.button_openoutputdir.Text = ">";
            this.button_openoutputdir.UseVisualStyleBackColor = true;
            this.button_openoutputdir.Click += new System.EventHandler(this.button_openoutputdir_Click);
            // 
            // button_openfile
            // 
            this.button_openfile.Location = new System.Drawing.Point(1248, 277);
            this.button_openfile.Margin = new System.Windows.Forms.Padding(7, 7, 7, 7);
            this.button_openfile.Name = "button_openfile";
            this.button_openfile.Size = new System.Drawing.Size(47, 45);
            this.button_openfile.TabIndex = 40;
            this.button_openfile.Text = ">";
            this.button_openfile.UseVisualStyleBackColor = true;
            this.button_openfile.Click += new System.EventHandler(this.button_openfile_Click_1);
            // 
            // button_openinputdir
            // 
            this.button_openinputdir.Location = new System.Drawing.Point(1248, 205);
            this.button_openinputdir.Margin = new System.Windows.Forms.Padding(7, 7, 7, 7);
            this.button_openinputdir.Name = "button_openinputdir";
            this.button_openinputdir.Size = new System.Drawing.Size(47, 45);
            this.button_openinputdir.TabIndex = 39;
            this.button_openinputdir.Text = ">";
            this.button_openinputdir.UseVisualStyleBackColor = true;
            this.button_openinputdir.Click += new System.EventHandler(this.button_openinputdir_Click);
            // 
            // textBox_iterations
            // 
            this.textBox_iterations.Location = new System.Drawing.Point(632, 413);
            this.textBox_iterations.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.textBox_iterations.Name = "textBox_iterations";
            this.textBox_iterations.Size = new System.Drawing.Size(163, 35);
            this.textBox_iterations.TabIndex = 37;
            this.textBox_iterations.Text = "3";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(483, 419);
            this.label7.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(111, 29);
            this.label7.TabIndex = 36;
            this.label7.Text = "Iterations";
            // 
            // checkBox_multiple
            // 
            this.checkBox_multiple.AutoSize = true;
            this.checkBox_multiple.Location = new System.Drawing.Point(75, 125);
            this.checkBox_multiple.Margin = new System.Windows.Forms.Padding(7, 7, 7, 7);
            this.checkBox_multiple.Name = "checkBox_multiple";
            this.checkBox_multiple.Size = new System.Drawing.Size(382, 33);
            this.checkBox_multiple.TabIndex = 34;
            this.checkBox_multiple.Text = "Read and process multiple files";
            this.checkBox_multiple.UseVisualStyleBackColor = true;
            this.checkBox_multiple.CheckedChanged += new System.EventHandler(this.checkBox_multiple_CheckedChanged);
            // 
            // radioButton_chisquare
            // 
            this.radioButton_chisquare.AutoSize = true;
            this.radioButton_chisquare.Checked = true;
            this.radioButton_chisquare.Location = new System.Drawing.Point(103, 415);
            this.radioButton_chisquare.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.radioButton_chisquare.Name = "radioButton_chisquare";
            this.radioButton_chisquare.Size = new System.Drawing.Size(164, 33);
            this.radioButton_chisquare.TabIndex = 33;
            this.radioButton_chisquare.TabStop = true;
            this.radioButton_chisquare.Text = "Chi Square";
            this.radioButton_chisquare.UseVisualStyleBackColor = true;
            this.radioButton_chisquare.CheckedChanged += new System.EventHandler(this.radioButton_chisquare_CheckedChanged);
            // 
            // radioButton_bayesian
            // 
            this.radioButton_bayesian.AutoSize = true;
            this.radioButton_bayesian.Location = new System.Drawing.Point(103, 471);
            this.radioButton_bayesian.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.radioButton_bayesian.Name = "radioButton_bayesian";
            this.radioButton_bayesian.Size = new System.Drawing.Size(142, 33);
            this.radioButton_bayesian.TabIndex = 32;
            this.radioButton_bayesian.Text = "Bayesian";
            this.radioButton_bayesian.UseVisualStyleBackColor = true;
            // 
            // textBox_ThresHoldCriterium
            // 
            this.textBox_ThresHoldCriterium.Location = new System.Drawing.Point(632, 468);
            this.textBox_ThresHoldCriterium.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.textBox_ThresHoldCriterium.Name = "textBox_ThresHoldCriterium";
            this.textBox_ThresHoldCriterium.Size = new System.Drawing.Size(163, 35);
            this.textBox_ThresHoldCriterium.TabIndex = 31;
            this.textBox_ThresHoldCriterium.Text = "1";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(399, 475);
            this.label6.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(225, 29);
            this.label6.TabIndex = 30;
            this.label6.Text = "ThresHoldCriterium";
            // 
            // label_result
            // 
            this.label_result.AutoSize = true;
            this.label_result.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_result.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label_result.Location = new System.Drawing.Point(40, 1211);
            this.label_result.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label_result.Name = "label_result";
            this.label_result.Size = new System.Drawing.Size(0, 40);
            this.label_result.TabIndex = 29;
            // 
            // graph_linkage
            // 
            this.graph_linkage.Location = new System.Drawing.Point(1309, 60);
            this.graph_linkage.Margin = new System.Windows.Forms.Padding(7, 7, 7, 7);
            this.graph_linkage.Name = "graph_linkage";
            this.graph_linkage.Size = new System.Drawing.Size(1304, 453);
            this.graph_linkage.TabIndex = 13;
            this.graph_linkage.TabStop = false;
            // 
            // result_info
            // 
            this.result_info.Location = new System.Drawing.Point(44, 544);
            this.result_info.Margin = new System.Windows.Forms.Padding(7, 7, 7, 7);
            this.result_info.Multiline = true;
            this.result_info.Name = "result_info";
            this.result_info.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.result_info.Size = new System.Drawing.Size(942, 682);
            this.result_info.TabIndex = 28;
            // 
            // file_info
            // 
            this.file_info.Location = new System.Drawing.Point(1008, 544);
            this.file_info.Margin = new System.Windows.Forms.Padding(7, 7, 7, 7);
            this.file_info.Multiline = true;
            this.file_info.Name = "file_info";
            this.file_info.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.file_info.Size = new System.Drawing.Size(1602, 682);
            this.file_info.TabIndex = 27;
            // 
            // button_run
            // 
            this.button_run.Location = new System.Drawing.Point(852, 468);
            this.button_run.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.button_run.Name = "button_run";
            this.button_run.Size = new System.Drawing.Size(385, 49);
            this.button_run.TabIndex = 20;
            this.button_run.Text = "Start Axon Server Sync";
            this.button_run.UseVisualStyleBackColor = true;
            this.button_run.Click += new System.EventHandler(this.button_run_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(68, 277);
            this.label4.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(156, 29);
            this.label4.TabIndex = 26;
            this.label4.Text = "Data file (.prt)";
            // 
            // textBox_filename
            // 
            this.textBox_filename.Location = new System.Drawing.Point(299, 272);
            this.textBox_filename.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.textBox_filename.Name = "textBox_filename";
            this.textBox_filename.Size = new System.Drawing.Size(531, 35);
            this.textBox_filename.TabIndex = 25;
            this.textBox_filename.Text = "test random.prt";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(68, 348);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(183, 29);
            this.label2.TabIndex = 24;
            this.label2.Text = "Output directory";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(68, 207);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(164, 29);
            this.label1.TabIndex = 23;
            this.label1.Text = "Input directory";
            // 
            // textBox_outputdir
            // 
            this.textBox_outputdir.Location = new System.Drawing.Point(299, 344);
            this.textBox_outputdir.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.textBox_outputdir.Name = "textBox_outputdir";
            this.textBox_outputdir.Size = new System.Drawing.Size(930, 35);
            this.textBox_outputdir.TabIndex = 22;
            this.textBox_outputdir.Text = "D:\\axon\\output";
            // 
            // textBox_inputdir
            // 
            this.textBox_inputdir.Location = new System.Drawing.Point(299, 205);
            this.textBox_inputdir.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.textBox_inputdir.Name = "textBox_inputdir";
            this.textBox_inputdir.Size = new System.Drawing.Size(930, 35);
            this.textBox_inputdir.TabIndex = 21;
            this.textBox_inputdir.Text = "D:\\axon\\input";
            // 
            // debugInstructionsLabel
            // 
            this.debugInstructionsLabel.AutoSize = true;
            this.debugInstructionsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.debugInstructionsLabel.Location = new System.Drawing.Point(68, 27);
            this.debugInstructionsLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.debugInstructionsLabel.Name = "debugInstructionsLabel";
            this.debugInstructionsLabel.Size = new System.Drawing.Size(576, 39);
            this.debugInstructionsLabel.TabIndex = 19;
            this.debugInstructionsLabel.Text = "Input an output directory and datafile.";
            // 
            // Test
            // 
            this.Test.BackColor = System.Drawing.Color.Gainsboro;
            this.Test.Controls.Add(this.textBox_N);
            this.Test.Controls.Add(this.labelN);
            this.Test.Controls.Add(this.labelVn);
            this.Test.Controls.Add(this.labelCn);
            this.Test.Controls.Add(this.textBox_Cn);
            this.Test.Controls.Add(this.textBox_Vn);
            this.Test.Controls.Add(this.label5);
            this.Test.Controls.Add(this.textBox_outputdir_random);
            this.Test.Controls.Add(this.label3);
            this.Test.Controls.Add(this.textBox_filename_random2);
            this.Test.Controls.Add(this.button_random);
            this.Test.Location = new System.Drawing.Point(10, 47);
            this.Test.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Test.Name = "Test";
            this.Test.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Test.Size = new System.Drawing.Size(2663, 1264);
            this.Test.TabIndex = 1;
            this.Test.Text = "Test";
            // 
            // textBox_N
            // 
            this.textBox_N.Location = new System.Drawing.Point(422, 230);
            this.textBox_N.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.textBox_N.Name = "textBox_N";
            this.textBox_N.Size = new System.Drawing.Size(116, 35);
            this.textBox_N.TabIndex = 36;
            this.textBox_N.Text = "10000";
            // 
            // labelN
            // 
            this.labelN.AutoSize = true;
            this.labelN.Location = new System.Drawing.Point(203, 234);
            this.labelN.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.labelN.Name = "labelN";
            this.labelN.Size = new System.Drawing.Size(183, 29);
            this.labelN.TabIndex = 35;
            this.labelN.Text = "N record blocks";
            // 
            // labelVn
            // 
            this.labelVn.AutoSize = true;
            this.labelVn.Location = new System.Drawing.Point(203, 299);
            this.labelVn.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.labelVn.Name = "labelVn";
            this.labelVn.Size = new System.Drawing.Size(134, 29);
            this.labelVn.TabIndex = 34;
            this.labelVn.Text = "N variables";
            // 
            // labelCn
            // 
            this.labelCn.AutoSize = true;
            this.labelCn.Location = new System.Drawing.Point(203, 359);
            this.labelCn.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.labelCn.Name = "labelCn";
            this.labelCn.Size = new System.Drawing.Size(150, 29);
            this.labelCn.TabIndex = 33;
            this.labelCn.Text = "N categories";
            // 
            // textBox_Cn
            // 
            this.textBox_Cn.Location = new System.Drawing.Point(422, 355);
            this.textBox_Cn.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.textBox_Cn.Name = "textBox_Cn";
            this.textBox_Cn.Size = new System.Drawing.Size(116, 35);
            this.textBox_Cn.TabIndex = 32;
            this.textBox_Cn.Text = "20";
            // 
            // textBox_Vn
            // 
            this.textBox_Vn.Location = new System.Drawing.Point(422, 294);
            this.textBox_Vn.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.textBox_Vn.Name = "textBox_Vn";
            this.textBox_Vn.Size = new System.Drawing.Size(116, 35);
            this.textBox_Vn.TabIndex = 31;
            this.textBox_Vn.Text = "5";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(203, 69);
            this.label5.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(164, 29);
            this.label5.TabIndex = 30;
            this.label5.Text = "Input directory";
            // 
            // textBox_outputdir_random
            // 
            this.textBox_outputdir_random.Location = new System.Drawing.Point(422, 65);
            this.textBox_outputdir_random.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.textBox_outputdir_random.Name = "textBox_outputdir_random";
            this.textBox_outputdir_random.Size = new System.Drawing.Size(930, 35);
            this.textBox_outputdir_random.TabIndex = 29;
            this.textBox_outputdir_random.Text = "D:/axon/input";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(203, 154);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(156, 29);
            this.label3.TabIndex = 28;
            this.label3.Text = "Data file (.prt)";
            // 
            // textBox_filename_random2
            // 
            this.textBox_filename_random2.Location = new System.Drawing.Point(422, 149);
            this.textBox_filename_random2.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.textBox_filename_random2.Name = "textBox_filename_random2";
            this.textBox_filename_random2.Size = new System.Drawing.Size(531, 35);
            this.textBox_filename_random2.TabIndex = 27;
            this.textBox_filename_random2.Text = "test random.prt";
            // 
            // button_random
            // 
            this.button_random.Location = new System.Drawing.Point(1104, 344);
            this.button_random.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.button_random.Name = "button_random";
            this.button_random.Size = new System.Drawing.Size(226, 62);
            this.button_random.TabIndex = 21;
            this.button_random.Text = "Build random file";
            this.button_random.UseVisualStyleBackColor = true;
            this.button_random.Click += new System.EventHandler(this.button_random_Click_1);
            // 
            // checkBox_CreateGraph
            // 
            this.checkBox_CreateGraph.AutoSize = true;
            this.checkBox_CreateGraph.Location = new System.Drawing.Point(633, 125);
            this.checkBox_CreateGraph.Name = "checkBox_CreateGraph";
            this.checkBox_CreateGraph.Size = new System.Drawing.Size(269, 33);
            this.checkBox_CreateGraph.TabIndex = 45;
            this.checkBox_CreateGraph.Text = "Create linkage graph";
            this.checkBox_CreateGraph.UseVisualStyleBackColor = true;
            // 
            // AxonForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2763, 1419);
            this.Controls.Add(this.textBox_filename_random);
            this.Controls.Add(this.helloWorldLabel);
            this.Controls.Add(this.label_talcott);
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "AxonForm";
            this.Text = "C:/Axon";
            this.textBox_filename_random.ResumeLayout(false);
            this.Run.ResumeLayout(false);
            this.Run.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.graph_linkage)).EndInit();
            this.Test.ResumeLayout(false);
            this.Test.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel label_talcott;
        private System.Windows.Forms.Label helloWorldLabel;
        private System.Windows.Forms.TabControl textBox_filename_random;
        private System.Windows.Forms.TabPage Run;
        private System.Windows.Forms.Button button_run;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_filename;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_outputdir;
        private System.Windows.Forms.TextBox textBox_inputdir;
        private System.Windows.Forms.Label debugInstructionsLabel;
        private System.Windows.Forms.TabPage Test;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox_outputdir_random;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_filename_random2;
        private System.Windows.Forms.Button button_random;
        private System.Windows.Forms.TextBox textBox_Vn;
        private System.Windows.Forms.TextBox textBox_Cn;
        private System.Windows.Forms.TextBox textBox_N;
        private System.Windows.Forms.Label labelN;
        private System.Windows.Forms.Label labelVn;
        private System.Windows.Forms.Label labelCn;
        private System.Windows.Forms.TextBox file_info;
        private System.Windows.Forms.TextBox result_info;
        private System.Windows.Forms.PictureBox graph_linkage;
        private System.Windows.Forms.Label label_result;
        private System.Windows.Forms.TextBox textBox_ThresHoldCriterium;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RadioButton radioButton_bayesian;
        private System.Windows.Forms.RadioButton radioButton_chisquare;
        private System.Windows.Forms.TextBox textBox_iterations;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button_openinputdir;
        private System.Windows.Forms.Button button_openfile;
        private System.Windows.Forms.Button button_openoutputdir;
        private System.Windows.Forms.Button button_run_async;
        private System.Windows.Forms.CheckBox checkBox_multiple;
        private System.Windows.Forms.CheckBox checkBox_CreateGraph;
    }
}

