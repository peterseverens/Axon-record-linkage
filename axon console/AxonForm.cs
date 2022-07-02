using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;

// This is the code for your desktop app.
// Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.

namespace axon_console
{

    public partial class AxonForm : Form
    {

      

        DateTime startDateTime;
        DateTime endDateTime;
        int linkmethodNow;

        public AxonForm()
        {
            InitializeComponent();
      
            //set build to 64 bit!! 
             

            System.Collections.IDictionary results = Environment.GetEnvironmentVariables();
            string resultS0 = Environment.GetEnvironmentVariable("gcAllowVeryLargeObjects,");
 
            Environment.SetEnvironmentVariable("gcAllowVeryLargeObjects", "true");

            //just checking wether environment variables are set for 64 bit vars
            string resultS1 = Environment.GetEnvironmentVariable("gcAllowVeryLargeObjects");
            System.Collections.IDictionary resultI = Environment.GetEnvironmentVariables();

            

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //Go to Talcott.nl

            System.Diagnostics.Process.Start("http://talcott.nl");
        }

        private void button_run_Click(object sender, EventArgs e)
        {

            //RUN SYNC

            startDateTime = DateTime.Now;
            textBox_inputdir.Focus();
            label_result.Text = "Still busy, running Sync !!";
            result_info.Text = "";
            file_info.Text = "";
            Application.DoEvents();

            byte iterateN = Convert.ToByte(textBox_iterations.Text);
            if (radioButton_bayesian.Checked) { linkmethodNow = 0; } else { linkmethodNow = 1; }

            finalResults result = axonGoSync(textBox_inputdir.Text, textBox_filename.Text, textBox_outputdir.Text, "d:/axon/cache", "/", checkBox_multiple.Checked, checkBox_CreateGraph.Checked, iterateN);

            if (result.error)
            {
                label_result.Text = result.result;
            }
            else
            {
                endDateTime = DateTime.Now;
                label_result.Text = "Your output is ready! duration: " + (endDateTime - startDateTime).ToString();
            }

        }

        private void button_run_async_Click(object sender, EventArgs e)
        {

            //RUN ASYNC

            startDateTime = DateTime.Now;
            textBox_inputdir.Focus();
            label_result.Text = "Still busy, running Async!!";
            result_info.Text = "";
            file_info.Text = "";
            Application.DoEvents();

            byte iterateN = Convert.ToByte(textBox_iterations.Text);
            if (radioButton_bayesian.Checked) { linkmethodNow = 0; } else { linkmethodNow = 1; }

            axonGoAsync(textBox_inputdir.Text, textBox_filename.Text, textBox_outputdir.Text, "d:/axon/cache", "/", checkBox_multiple.Checked, checkBox_CreateGraph.Checked, iterateN);

            endDateTime = DateTime.Now;
            label_result.Text = "Your output is ready! duration: " + (endDateTime - startDateTime).ToString();
        }
        public void axonGoAsync(string inputDir, string inputFile, string outputDir, string cachDir, string delimeter, Boolean multi, Boolean graph ,byte iterateN)
        {

 
            //CHECK WETHER MULTIPLE FILES ARE TO BE READ
            string[] files = new string[1];
            if (!multi)
            {
                files[0] = inputFile;
            
            }
            else
            {               
                try
                {
                    files = Directory.GetFiles(inputDir, "*.prt", SearchOption.AllDirectories);

                }
                catch
                {
                    //Ignore folder (access denied).
                    //rootDirectory = null;
                }
            }
            for (UInt16 i = 0; i < files.Length; i++)
            {
                files[i] = Path.GetFileNameWithoutExtension(files[i]);
            }

            Task[] task = new Task[files.Length];

            UInt16[] taskId = new UInt16[Int16.MaxValue];

            Double ThresHoldCriteriumNow = Convert.ToDouble(textBox_ThresHoldCriterium.Text);

            for (UInt16 i = 0; i < files.Length; i++)

            {

                UInt16 x = i;
                task[i] = Task.Run(() => {
                    
                    UInt16 iNow = x;


                    AxonCalc11 AxC = new AxonCalc11();


                    AxC.Delimeter = '/';
                    AxC.CategoryFrequenciesShow = 1;


                    AxC.Linkmethod = linkmethodNow;
                    AxC.ThresHoldCriterium = ThresHoldCriteriumNow;
              
                    AxC.CriteriumHandling = 3; //get all reocords above threshold

                    //!!!!!!!!!!!
                    //AxC.ThresHoldPercentageCriterium = 0;
                    //AxC.MissingsInclude = 1;  //missing categories are also used to calculate Likelihood
                    //AxC.DifferenceCriterium = -99;

                    AxC.chi2WithinShow = 0;
                    AxC.Chi2BetweenShow = 1;
                    AxC.SelectedIdentifiersShow = 1;
                   
                    AxC.LinkedSetsShow = 0;
                    AxC.DistributionLinkedRecordsNShow = 1;
                    if (graph) { AxC.DistributionLikelihoodsShow = 1; } else { AxC.DistributionLikelihoodsShow = 0; };


                    string inputFileNow = inputDir + "\\" + files[iNow] + ".prt"; ;
                    string inputFileCopy = outputDir + "\\" + files[iNow] + ".prc";
                    string linkReportFileInfo = outputDir + "\\" + files[iNow] + ".fnf";
                    string linkReportFileFreq = outputDir + "\\" + files[iNow] + ".frq";
                    string linkOutputFile = outputDir + "\\" + files[iNow] + ".lin";
                    string linkReportFile = outputDir + "\\" + files[iNow] + ".rpt";
                    string linkGraphFile = outputDir + "\\" + files[iNow] + ".gra";



                    StreamWriter fileInfo = new System.IO.StreamWriter(linkReportFileInfo);
                    StreamWriter fileFreq = new System.IO.StreamWriter(linkReportFileFreq);
                    StreamWriter fileReport = new System.IO.StreamWriter(linkReportFile);
                    StreamWriter fileScores = new System.IO.StreamWriter(linkOutputFile);
                    StreamWriter fileGraph = new System.IO.StreamWriter(linkGraphFile);
                     

                    StreamReader fileIn = new System.IO.StreamReader(inputFileNow);

                    dataCheckResults resultDataCheck = AxC.SetsGetDelimetedCountAndCheck(fileIn);


                    UInt64 Ns = resultDataCheck.Ns;
                    UInt64 Nr = resultDataCheck.Nr;
                    UInt16 Vc0 = resultDataCheck.Vc0;
                    UInt16 Vc = resultDataCheck.Vc;
                    UInt16 Vc1 = resultDataCheck.Vc1;
                    UInt64 rpos = resultDataCheck.rpos;



                    AxC.arraysIniGeneric(Vc0, Vc, Vc1);

                    string resultArraysData = AxC.arraysIniData();
                    if (resultArraysData != "ok")
                    {
                        result_info.Text = "cannot create data arrays. Total number of blocks: " + Ns.ToString() + " Total number of records: " + Nr.ToString();
                        return;
                    }


                    fileIn = new System.IO.StreamReader(inputFileNow);
                    dataReadResults resulDataReading = AxC.SetsGetDelimetedReadAllData(fileIn,  rpos);

                    Ns = resulDataReading.Ns;
                    Nr = resulDataReading.Nr;

                    if (Ns > 0)
                    {
                        if (multi)
                        {
                            StreamWriter fileCopy = new System.IO.StreamWriter(inputFileCopy);
                            if (iNow == 0)
                            {
                                AxC.SetsWriteDelimeted(fileCopy, delimeter, false);
                            }
                            else
                            {
                                AxC.SetsWriteDelimeted(fileCopy, delimeter, true);
                            }
                            fileCopy.Close();
                        }
                       

                        fileInfo.Write(AxC.OutputFileInfo(inputFileNow));
                        fileInfo.Close();

                        fileFreq.Write(AxC.OutputFrequencies());
                        fileFreq.Close();

                        Application.DoEvents();
                        UInt64[] result = AxC.countTotalCategories();


                        string resultArraysLinkage = AxC.arraysIniLinkage(result[0], result[1]);

                        if (resultArraysLinkage == "ok")
                        {

                        }
                        else
                        {

                            result_info.Text = "cannot create probability arrays. Total number of categories to cross : " + result[0].ToString();
                            return;
                        }



                        char dChar = AxC.Delimeter;


                        Graphics g = graph_linkage.CreateGraphics();
                        g.Clear(System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255))))));
                        Application.DoEvents();
                        for (byte ii = 0; ii < iterateN; ii++)
                        {


                            StringBuilder outputResult = AxC.Probabilities(ii);
                            fileReport.Write(outputResult);
                            //reporting                 result_info.Text += outputResult.ToString();
                            outputResult.Clear();
                            Boolean writeLinkage = false;
                            if (iterateN == ii + 1) writeLinkage = true;


                            StringBuilder outputLinkReport = AxC.LinkRecords(AxC.CriteriumHandling, AxC.ThresHoldCriterium, AxC.ThresHoldPercentageCriterium, ii, AxC.Linkmethod, fileScores, writeLinkage);
                            fileReport.Write(outputLinkReport);


                            float percentageLinked = Convert.ToSingle(AxC.blocksLinkedN) / Convert.ToSingle(AxC.Ns);

                            if (graph) writeGraphScores(AxC.scoresN, AxC.scoresV, AxC.Nsmall, AxC.Linkmethod, AxC.ThresHoldCriterium, percentageLinked, fileGraph);


                        }
                        fileInfo.Close();
                        fileFreq.Close();
                        fileReport.Close();
                        fileScores.Close();
                        fileGraph.Close();


                    }
                    taskId[task[iNow].Id] = iNow;
                });
            }

            UInt16 readyN = 0;
            do
            {
                readyN = 0;
                Thread.Sleep(2000);
                file_info.Clear();
                result_info.Clear();
                //Application.DoEvents();
                string text = "";
                foreach (var t in task)
                {

                    text += "\r\n" + files[taskId[t.Id]] + ": " + t.Status;

                    if (t.IsCompleted)
                    {
                        StringBuilder fileInfoFromFile;
                        readyN += 1;

                        fileInfoFromFile = getFileContent(outputDir + "\\" + files[taskId[t.Id]] + ".fnf");

                        file_info.Text += fileInfoFromFile.ToString();

                    };

                    result_info.Text = "task finished: " + readyN.ToString() + " from " + files.Length.ToString() + "\r\n" + text;
                }
                Application.DoEvents();
                //Thread.Sleep(1000) ;
            }
            while (readyN < files.Length);

            try
            {
                Task.WaitAll(task);

            }
            catch (AggregateException ae)
            {
                Console.WriteLine("One or more exceptions occurred: ");
                foreach (var ex in ae.Flatten().InnerExceptions)
                    Console.WriteLine("   {0}", ex.Message);
            }

            if (multi)
            {
                string[] filesNow = new string[files.Length];
                foreach (var t in task)
                {
                    filesNow[taskId[t.Id]] = outputDir + "\\" + files[taskId[t.Id]] + ".lin";
                }

                string fileOutNow = outputDir + "\\" + "combined " + Path.GetFileName(inputDir) + ".linc";
                combineFiles(filesNow, fileOutNow);

                filesNow = new string[files.Length];
                foreach (var t in task)
                {
                    filesNow[taskId[t.Id]] = outputDir + "\\" + files[taskId[t.Id]] + ".prc";
                }

                fileOutNow = outputDir + "\\" + "combined " + Path.GetFileName(inputDir) + ".prtc";
                combineFiles(filesNow, fileOutNow);


            }
            if (graph)
            {
                string[] filesGra = new string[files.Length];
                foreach (var t in task)
                {
                    filesGra[taskId[t.Id]] = outputDir + "\\" + files[taskId[t.Id]] + ".Gra";
                }
                GraphLikelihoods(filesGra, multi, linkmethodNow);
            }


            for (UInt16 i = 0; i < files.Length; i++) { task[i].Dispose(); }


        }

        public finalResults axonGoSync(string inputDir, string inputFile, string outputDir, string cachDir, string delimeter, Boolean multi, Boolean graph, byte iterateN)
        {


            finalResults procResult = new finalResults();

            //CHECK WETHER MULTIPLE FILES ARE TO BE READ
            string[] files = new string[1];
            if (!multi)
            {
                files[0] = inputFile;
                
            }
            else
            {
                
                try
                {
                    files = Directory.GetFiles(inputDir, "*.prt", SearchOption.AllDirectories);

                }
                catch
                {
                    //Ignore folder (access denied).
                    //rootDirectory = null;
                }
            }
            for (UInt16 i = 0; i < files.Length; i++)
            {
                files[i] = Path.GetFileNameWithoutExtension(files[i]);
            }
             
            Double ThresHoldCriteriumNow = Convert.ToDouble(textBox_ThresHoldCriterium.Text);

            string text = "";
            for (UInt16 i = 0; i < files.Length; i++)

            {

                UInt16 iNow = i;


                AxonCalc11 AxC = new AxonCalc11();


                AxC.Delimeter = '/';
                AxC.CategoryFrequenciesShow = 1;
                 

                AxC.Linkmethod = linkmethodNow;
                AxC.ThresHoldCriterium = ThresHoldCriteriumNow;
 
                AxC.CriteriumHandling = 3; //get all reocords above threshold
                
                //!!!!!!!!!!!
                //AxC.ThresHoldPercentageCriterium = 0;
                //AxC.MissingsInclude = 1;  //missing categories are also used to calculate Likelihood
                //AxC.DifferenceCriterium = -99;

                AxC.chi2WithinShow = 0;
                AxC.Chi2BetweenShow = 1;
                AxC.SelectedIdentifiersShow = 1;
 
                AxC.LinkedSetsShow = 0;
                AxC.DistributionLinkedRecordsNShow = 1;
                if (graph) { AxC.DistributionLikelihoodsShow = 1; } else { AxC.DistributionLikelihoodsShow = 0; };



                string inputFileNow = inputDir + "\\" + files[iNow] + ".prt"; ;
                string inputFileCopy = outputDir + "\\" + files[iNow] + ".prc";
                string linkReportFileInfo = outputDir + "\\" + files[iNow] + ".fnf";
                string linkReportFileFreq = outputDir + "\\" + files[iNow] + ".frq";
                string linkOutputFile = outputDir + "\\" + files[iNow] + ".lin";
                string linkReportFile = outputDir + "\\" + files[iNow] + ".rpt";
                string linkGraphFile = outputDir + "\\" + files[iNow] + ".gra";



                StreamWriter fileInfo = new System.IO.StreamWriter(linkReportFileInfo);
                StreamWriter fileFreq = new System.IO.StreamWriter(linkReportFileFreq);
                StreamWriter fileReport = new System.IO.StreamWriter(linkReportFile);
                StreamWriter fileScores = new System.IO.StreamWriter(linkOutputFile);
                StreamWriter fileGraph = new System.IO.StreamWriter(linkGraphFile);
                 
                StreamReader fileIn = new System.IO.StreamReader(inputFileNow);
  
                dataCheckResults resultDataCheck = AxC.SetsGetDelimetedCountAndCheck(fileIn);

                if (resultDataCheck.error) { 
                    result_info.Text = resultDataCheck.result;
                    procResult.result = resultDataCheck.result;
                    procResult.error = true;
                    return procResult; 
                }

                UInt64 Ns = resultDataCheck.Ns;
                UInt64 Nr = resultDataCheck.Nr;
                UInt16 Vc0 = resultDataCheck.Vc0;
                UInt16 Vc = resultDataCheck.Vc;
                UInt16 Vc1 = resultDataCheck.Vc1;
                UInt64 rpos = resultDataCheck.rpos;


                AxC.arraysIniGeneric(Vc0, Vc, Vc1);

                string resultArraysData = AxC.arraysIniData();
                if (resultArraysData != "ok")
                {

                    procResult.error = true;
                    procResult.result = "insufficient memory: cannot create data arrays. Total number of blocks: " + Ns.ToString() + " Total number of records: " + Nr.ToString();
                    return procResult;
                }


                fileIn = new System.IO.StreamReader(inputFileNow);
                dataReadResults resulDataReading = AxC.SetsGetDelimetedReadAllData(fileIn,  rpos);

                Ns = resulDataReading.Ns;
                Nr = resulDataReading.Nr;


                if (Ns > 0)
                {
                    if (multi)
                    {
                        StreamWriter fileCopy = new System.IO.StreamWriter(inputFileCopy);
                        if (iNow == 0)
                        {
                            AxC.SetsWriteDelimeted(fileCopy, delimeter, false);
                        }
                        else
                        {
                            AxC.SetsWriteDelimeted(fileCopy, delimeter, true);
                        }
                        fileCopy.Close();
                    }
                   

                    fileInfo.Write(AxC.OutputFileInfo(inputFileNow));
                    fileInfo.Close();

                    fileFreq.Write(AxC.OutputFrequencies());
                    fileFreq.Close();

                    Application.DoEvents();
                    UInt64[] result = AxC.countTotalCategories();


                    string resultArraysLinkage = AxC.arraysIniLinkage(result[0], result[1]);
                    //result_info.Text = sb2.ToString();

                    if (resultArraysLinkage != "ok")
                    {
                   

                        procResult.result = "insufficient memory: cannot create probability arrays. Total number of categories to cross : " + result[0].ToString();
                        return procResult;
                    }



                    char dChar = AxC.Delimeter;


                    Graphics g = graph_linkage.CreateGraphics();
                    g.Clear(System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255))))));
                    Application.DoEvents();


                    for (byte ii = 0; ii < iterateN; ii++)
                    {




                        StringBuilder outputResult = AxC.Probabilities(ii);
                        fileReport.Write(outputResult);
                        //reporting                 result_info.Text += outputResult.ToString();
                        outputResult.Clear();
                        Boolean writeLinkage = false;
                        if (iterateN == ii + 1) writeLinkage = true;


                        StringBuilder outputLinkReport = AxC.LinkRecords(AxC.CriteriumHandling, AxC.ThresHoldCriterium, AxC.ThresHoldPercentageCriterium, ii, AxC.Linkmethod, fileScores, writeLinkage);
                        fileReport.Write(outputLinkReport);


                        float percentageLinked = Convert.ToSingle(AxC.blocksLinkedN) / Convert.ToSingle(AxC.Ns);

                        if (graph) writeGraphScores(AxC.scoresN, AxC.scoresV, AxC.Nsmall, AxC.Linkmethod, AxC.ThresHoldCriterium, percentageLinked, fileGraph);


                    }
                    fileInfo.Close();
                    fileFreq.Close();
                    fileReport.Close();
                    fileScores.Close();
                    fileGraph.Close();

                }

                text += "\r\n" + files[i] + ": finished ";
                result_info.Text = "task finished: " + i.ToString() + " from " + files.Length.ToString() + "\r\n" + text;
                Application.DoEvents();

            }

            UInt16 readyN = 0;

            file_info.Clear();
            //result_info.Clear();
            //Application.DoEvents();
            // string text = "";
            for (UInt16 i = 0; i < files.Length; i++)
            {

                StringBuilder fileInfoFromFile;
                readyN += 1;

                fileInfoFromFile = getFileContent(outputDir + "\\" + files[i] + ".fnf");

                file_info.Text += fileInfoFromFile.ToString();

            }
            Application.DoEvents();


            if (multi)
            {
                string[] filesNow = new string[files.Length];
                for (UInt16 i = 0; i < files.Length; i++)
                {
                    filesNow[i] = outputDir + "\\" + files[i] + ".lin";
                }

                string fileOutNow = outputDir + "\\" + "combined " + Path.GetFileName(inputDir) + ".linc";
                combineFiles(filesNow, fileOutNow);

                filesNow = new string[files.Length];
                for (UInt16 i = 0; i < files.Length; i++)
                {
                    filesNow[i] = outputDir + "\\" + files[i] + ".prc";
                }

                fileOutNow = outputDir + "\\" + "combined " + Path.GetFileName(inputDir) + ".prtc";
                combineFiles(filesNow, fileOutNow);


            }
            if (graph)
            {
                string[] filesGra = new string[files.Length];
                for (UInt16 i = 0; i < files.Length; i++)
                {
                    filesGra[i] = outputDir + "\\" + files[i] + ".Gra";
                }
                GraphLikelihoods(filesGra, multi, linkmethodNow);
            }
          
            return procResult;

        }

        private void button_random_Click(object sender, EventArgs e)
        {
        }

        private void BuildRandomFile(string outputFile, string cNin, string cVin, string cCin, string delimeter)
        {

            //BUILD ARANDOM TEST FILE

            AxonCalc11 AxC = new AxonCalc11();

            string fileName = outputFile;

            UInt64 Nin = 0;
            UInt16 Vin = 0;
            UInt16 Cin = 0;

            Nin = Convert.ToUInt64(cNin);
            Vin = Convert.ToUInt16(cVin);
            Cin = Convert.ToUInt16(cCin);

            if (Nin < 1000) { Nin = 10000; }
            if (Vin < 2) { Vin = 5; }
            if (Cin < 2) { Cin = 7; }

            AxC.SetsRandom(Nin, Vin, Cin);
            System.IO.StreamWriter file = new System.IO.StreamWriter(fileName);
            AxC.SetsWriteDelimeted(file, delimeter, false);
            file.Close();

            StringBuilder sb = AxC.OutputFileInfo(fileName);
            StringBuilder sb2 = AxC.OutputFrequencies();

            file_info.Text = sb.ToString();
            result_info.Text = sb2.ToString();
            //iterate();
            //FileDirData();
        }


        private void combineFiles(string[] files, string fileOutNow)
        {

            //COMBINE FILES FROM MULTI RESULT (multiple inputfiles in a directory) 

            System.IO.StreamWriter fileOut = new System.IO.StreamWriter(fileOutNow);

            for (UInt16 i = 0; i < files.Length; i++)
            {
                System.IO.StreamReader fileIn = new System.IO.StreamReader(files[i]);

                string line = "";
                while ((line = fileIn.ReadLine()) != null)
                {
                    fileOut.WriteLine(line);
                }
                fileIn.Close();
            }
            fileOut.Close();
        }

        private void writeGraphScores(double[] scoresN, double[] scoresV, Int64 nSmall, int Linkmethod, double ThresHoldCriterium, float percentageLinked, StreamWriter fileGraphNow)
        {
            //WRITE LIKELIHOOD DISTRIBUTION TO REPORT

            string line = nSmall.ToString() + "," + Linkmethod.ToString() + "," + ThresHoldCriterium.ToString("0.00", CultureInfo.InvariantCulture) + "," + percentageLinked.ToString("0.00", CultureInfo.InvariantCulture);
            fileGraphNow.WriteLine(line);
            line = "";
            for (UInt16 c = 0; c < 101; c++)
            {
                line += scoresN[c] + ",";
            }
            fileGraphNow.WriteLine(line);
            line = "";
            for (UInt16 c = 0; c < 101; c++)
            {
                line += scoresV[c] + ",";
            }
            fileGraphNow.WriteLine(line);
            line = "";
        }




 
        private void GraphLikelihoods(string[] fileNow, Boolean multi, int linkMethodNow)
        {

            //GRAPH LIKELIHOOD DISTRIBUTION TO SCREEN

            int[] iterNumber = new int[fileNow.Length];
            int[] nSmall = new int[fileNow.Length];
            int[] Linkmethod = new int[fileNow.Length];
            double[,] percentageLinked = new double[fileNow.Length, byte.MaxValue];
            double[] ThresHold = new double[fileNow.Length];
            double[,,] scoresN = new double[fileNow.Length, byte.MaxValue, 101];
            double[,,] scoresV = new double[fileNow.Length, byte.MaxValue, 101];
            double[] percentageLinkedLast = new double[fileNow.Length];
            double percentageLinkedLastMean = 0;
            float scoreL = 99999; float scoreH = -99999;

            for (UInt16 i = 0; i < fileNow.Length; i++)
            {
                System.IO.StreamReader readerNow = new System.IO.StreamReader(fileNow[i]);
                iterNumber[i] = 0;

                string line = "";
                while ((line = readerNow.ReadLine()) != null)
                {
                    string[] linePar = line.Split(',');
                    if (iterNumber[i] == 0)
                    {

                        nSmall[i] = Convert.ToInt32(linePar[0]);
                        Linkmethod[i] = Convert.ToInt32(linePar[1]);
                        ThresHold[i] = Convert.ToDouble(linePar[2]);

                    }
                    percentageLinked[i, iterNumber[i]] = Convert.ToDouble(linePar[3]);

                    line = readerNow.ReadLine();
                    string[] lineVal = line.Split(',');
                    for (UInt16 c = 0; c < 101; c++)
                    {
                        scoresN[i, iterNumber[i], c] = Convert.ToDouble(lineVal[c]);
                    }

                    line = readerNow.ReadLine();
                    string[] lineVal2 = line.Split(',');
                    for (UInt16 c = 0; c < 101; c++)
                    {
                        scoresV[i, iterNumber[i], c] = Convert.ToDouble(lineVal2[c]);
                        if (scoresV[i, iterNumber[i], c] < scoreL) { scoreL = Convert.ToSingle(scoresV[i, iterNumber[i], c]); }
                        if (scoresV[i, iterNumber[i], c] > scoreH) { scoreH = Convert.ToSingle(scoresV[i, iterNumber[i], c]); }

                    }
                    percentageLinkedLast[i] = percentageLinked[i, iterNumber[i]];
                    iterNumber[i] += 1;
                }

                readerNow.Close();
            }
            Font fontNow = new Font("Helvetica", 10, FontStyle.Bold);


            Brush[] brushNow = new Brush[5];
            brushNow[0] = new SolidBrush(System.Drawing.ColorTranslator.FromHtml("#87CEFA"));
            brushNow[1] = new SolidBrush(System.Drawing.ColorTranslator.FromHtml("#00BFFF"));
            brushNow[2] = new SolidBrush(System.Drawing.ColorTranslator.FromHtml("#4169E1"));
            brushNow[3] = new SolidBrush(System.Drawing.ColorTranslator.FromHtml("#0000CD"));
            brushNow[4] = new SolidBrush(System.Drawing.ColorTranslator.FromHtml("#191970"));

            Pen[] penNow = new Pen[5];
            penNow[0] = new Pen(System.Drawing.ColorTranslator.FromHtml("#87CEFA"), 1);
            penNow[1] = new Pen(System.Drawing.ColorTranslator.FromHtml("#00BFFF"), 1);
            penNow[2] = new Pen(System.Drawing.ColorTranslator.FromHtml("#4169E1"), 1);
            penNow[3] = new Pen(System.Drawing.ColorTranslator.FromHtml("#0000CD"), 1);
            penNow[4] = new Pen(System.Drawing.ColorTranslator.FromHtml("#191970"), 1);

            Pen penNowR = new Pen(System.Drawing.Color.Red, 1);
            Pen penNowB = new Pen(System.Drawing.Color.Blue, 1);


            for (UInt16 i = 0; i < fileNow.Length; i++)
            {
                percentageLinkedLastMean += percentageLinkedLast[i];
            }
            percentageLinkedLastMean = percentageLinkedLastMean / fileNow.Length;
            string t;

            Graphics g = graph_linkage.CreateGraphics();
            g.FillRectangle(new SolidBrush(Color.FromArgb(0, Color.Black)), graph_linkage.DisplayRectangle);

            float m = 10;
            float w = graph_linkage.Width;
            float h = graph_linkage.Height;
            float ww = w - 2 * m;
            float hw = h - 3 * m;



            //Point[] points = new Point[4];
            //points[0] = new Point(m, m);
            double HighNow = 0;
            float hv = -99999; float lv = 999999;
            //multi = false;
            if (multi)
            {


                string gTitel = "";
                //string t = "";
                switch (Linkmethod[0])
                {
                    case -1:
                        t = "Distrib. of Likelihoods (obs/exp) : total link %: " + percentageLinkedLastMean.ToString("0.00", CultureInfo.InvariantCulture); break;
                    case 0:
                        gTitel = "Distrib. of Likelihoods : step : total link %: " + percentageLinkedLastMean.ToString("0.00", CultureInfo.InvariantCulture); break;
                    case 1:
                        gTitel = "Distrib. of CHI Squares (*10/n) : total link %: " + percentageLinkedLastMean.ToString("0.00", CultureInfo.InvariantCulture);
                        break;
                    default:
                        gTitel = "No linkmethod specified ";
                        break;

                }
                g.DrawString(gTitel, fontNow, brushNow[4], 4 * m, m + Convert.ToSingle(1.5) * m * Convert.ToSingle(0));

            }
            else
                for (UInt16 i = 0; i < fileNow.Length; i++)

                {
                    for (UInt16 iter = 0; iter < iterNumber[i]; iter++)
                    {
                        string gTitel = "";
                        //string t = "";
                        switch (Linkmethod[0])
                        {
                            case -1:
                                t = "Distrib. of Likelihoods (obs/exp) : step : "
                                            + iter.ToString("0", CultureInfo.InvariantCulture) + " (N = "
                                            + nSmall[0].ToString("0", CultureInfo.InvariantCulture) + " < -99), link %: " + percentageLinked[i, iter].ToString("0.00", CultureInfo.InvariantCulture);
                                break;
                            case 0:
                                gTitel = "Distrib. of Likelihoods : step : "
                                            + iter.ToString("0", CultureInfo.InvariantCulture) + " (N = "
                                            + nSmall[0].ToString("0.00", CultureInfo.InvariantCulture) + " < -99), link %: " + percentageLinked[i, iter].ToString("0.00", CultureInfo.InvariantCulture); ;
                                break;
                            case 1:
                                gTitel = "Distrib. of CHI Squares (*10/n) : step : " + iter.ToString("0", CultureInfo.InvariantCulture) + ", link %: " + percentageLinked[i, iter].ToString("0.00", CultureInfo.InvariantCulture); ;
                                break;
                            default:
                                gTitel = "No linkmethod specified ";
                                break;

                        }

                        if (iter < 4)
                        {
                            g.DrawString(gTitel, fontNow, brushNow[iter], 4 * m, m + Convert.ToSingle(1.5) * m * Convert.ToSingle(iter));
                        }
                        else
                        {
                            g.DrawString(gTitel, fontNow, brushNow[4], 4 * m, m + Convert.ToSingle(1.5) * m * Convert.ToSingle(iter));

                        }


                        for (int c = 0; (c < 101); c++)
                        {
                            if (scoresN[i, iter, c] > HighNow) { HighNow = scoresN[i, iter, c]; }
                            if (scoresV[i, iter, c] > hv) hv = Convert.ToSingle(scoresV[i, iter, c]);
                            if (scoresV[i, iter, c] < lv) lv = Convert.ToSingle(scoresV[i, iter, c]);
                        }

                    }

                }
            if (!multi)
            {
                for (UInt16 i = 0; i < fileNow.Length; i++)
                {
                    for (byte iter = 0; iter < iterNumber[i]; iter++)
                    {
                        float xOld = 0; float yOld = 0;
                        float hvn = -99999; float lvn = 999999;
                        for (UInt16 c = 0; c < 101; c++)
                        {

                            if (scoresV[i, iter, c] > hvn) hvn = Convert.ToSingle(scoresV[i, iter, c]);
                            if (scoresV[i, iter, c] < lvn) lvn = Convert.ToSingle(scoresV[i, iter, c]);

                        }
                        float sizeD = (hvn - lvn) / (hv - lv);
                        float startD = (lvn - lv) / (hv - lv);
                        float endD = (hv - hvn) / (hv - lv);
                        float wwAdjusted = ww * sizeD;
                        float wwStart = ww * startD;

                        float x = 0;
                        for (UInt16 c = 0; c < 101; c++)
                        {
                            if (sizeD == 1)
                            {
                                x = m + ww * c / 101;
                                double cutOff = 0; if (linkMethodNow == 1) cutOff = 1;

                                if (scoresV[i, iter, c] < cutOff)
                                {
                                    g.DrawLine(penNowR, x, hw + m + 2, x, hw + m + 4);
                                }
                                else
                                {
                                    g.DrawLine(penNowB, x, hw + m + 2, x, hw + m + 4);
                                }
                            }
                        }
                        if (i == 0 & iter == 0)
                        {
                            x = m;
                            g.DrawString(scoresV[i, iter, 0].ToString("0.00", CultureInfo.InvariantCulture), fontNow, brushNow[4], x, hw + m + 6);
                            x = m + ww;
                            string textH = scoresV[i, iter, 100].ToString("0.00", CultureInfo.InvariantCulture);
                            System.Drawing.SizeF fontW = g.MeasureString(textH, fontNow);
                            g.DrawString(textH, fontNow, brushNow[4], x - fontW.Width, hw + m + 6);
                        }

                        for (UInt16 c = 0; c < 101; c++)
                        {
                            x = m + wwStart + wwAdjusted * c / 101;
                            float y = m + hw - (hw * Convert.ToSingle(scoresN[i, iter, c]) / Convert.ToSingle(HighNow));

                            if (iter < 4) { g.DrawLine(penNow[iter], xOld, yOld, x, y); }
                            if (iter > 3) { g.DrawLine(penNow[4], xOld, yOld, x, y); }

                            xOld = x;
                            yOld = y;
                        }
                    }
                }
            }
        }

        private void Run_Click(object sender, EventArgs e)
        {

        }

        private void button_random_Click_1(object sender, EventArgs e)
        {
            BuildRandomFile(textBox_outputdir_random.Text + "/" + textBox_filename_random2.Text, textBox_N.Text, textBox_Vn.Text, textBox_Cn.Text, "/");

        }




        private void checkBox_multiple_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_multiple.Checked == true) { textBox_filename.Enabled = false; } else { textBox_filename.Enabled = true; };

        }

        private void radioButton_chisquare_CheckedChanged(object sender, EventArgs e)
        {

        }

        static string fileOpen(string[] args)
        {

            string fileName;
            OpenFileDialog fd = new OpenFileDialog();
            fd.ShowDialog();
            fileName = fd.FileName;
            return fileName;

        }


        public StringBuilder getFileContent(string fileNow)
        {
            StringBuilder sb = new StringBuilder();
            System.IO.StreamReader readerNow = new System.IO.StreamReader(fileNow);
            string line = "";
            while ((line = readerNow.ReadLine()) != null)
            {
                sb.Append(line + "\r\n");
            }
            readerNow.Close();
            return sb;
        }


        private void button_openfile_Click_1(object sender, EventArgs e)
        {
            

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = textBox_inputdir.Text;// "d:\\axon\\input";
                openFileDialog.Filter = "prt files (*.prt)|*.prt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    string strPath = openFileDialog.FileName;
                    textBox_filename.Text = Path.GetFileName(strPath);
                    textBox_inputdir.Text = Path.GetDirectoryName(strPath);
                    
                }
            }


        }
        private void button_openinputdir_Click(object sender, EventArgs e)
        {

            using (FolderBrowserDialog openDirDialog = new FolderBrowserDialog())
            {

                openDirDialog.SelectedPath = textBox_inputdir.Text; // "d:\\axon\\input";
                DialogResult result = openDirDialog.ShowDialog();


                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(openDirDialog.SelectedPath))
                {

                    string strPath = openDirDialog.SelectedPath;
                    textBox_inputdir.Text = strPath;
                }
            }



        }

        private void button_openoutputdir_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog openDirDialog = new FolderBrowserDialog())
            {

                openDirDialog.SelectedPath = textBox_outputdir.Text; // "d:\\axon\\output";
                DialogResult result = openDirDialog.ShowDialog();


                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(openDirDialog.SelectedPath))
                {

                    string strPath = openDirDialog.SelectedPath;
                    textBox_outputdir.Text = strPath;
                }
            }


        }


    }
}
