using System;

using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;



namespace axon_console
{
    public class AxonCalc10
    {

        //STATUS




        //ARRAY CONSTRAINTS

        public static UInt16 maxCat = UInt16.MaxValue;
        public static byte maxRperRecordS = byte.MaxValue;


        //Dbase file

        public char Delimeter;
        public string DataDir;
        public string BinDir;
        public string ResultDir;

        //?? Global DBS As Database
        //?? Global RST As Recordset


        public UInt64[,] freq0 = new UInt64[1, maxCat];
        public UInt64[,] freq1 = new UInt64[1, maxCat];
        public UInt64[,] freq = new UInt64[1, maxCat];

        public UInt16 maxVnow = 1;
        public UInt16 maxVnowS = 1;
        public UInt16 totVnow = 1;

        //GRAPHS

        public static byte NcatGraph = 101;
        public double[] scoresN = new double[0];
        public double[] scoresV = new double[0];

        public int Nsmall = 0;


        //ouput
        public string ReportID;
        public string SubReportID;
        public string t;

        //FILES
        //public string DBfile;
        public string rawfile;
        public string SysFile;
        public string OutputFile;
        public string ResultFile;
        //public string DBfileR;
        public string MetaScriptFile;
        public string ScriptFile;

        //DATALABELS

        public string[] Var0Labels = new string[1];  //vc0
        public string[] VarLabels = new string[1];   //vc
        public string[] Var1Labels = new string[1];  //vc1

        //MISSINGS

        public string[] missingS0 = new string[maxCat];
        public string[] missingS = new string[maxCat];
        public string[] missingS1 = new string[maxCat];

        public UInt16[] missing0 = new UInt16[maxCat];
        public UInt16[] missing = new UInt16[maxCat];
        public UInt16[] missing1 = new UInt16[maxCat];

        //VARIABLES

        public UInt16 Vc0;
        public UInt16 Vc;
        public UInt16 Vc1;
        public UInt16[] NcVc0 = new UInt16[1]; //vc0
        public UInt16[] NcVc = new UInt16[1]; //vc
        public UInt16[] NcVc1 = new UInt16[1];
        UInt16 NcVc0H = 0;
        UInt16 NcVcH = 0;
        UInt16 NcVc1H = 0;

        public UInt64 crossN = 0;  //total number of categories v0 * v1 * NcVc0[] * NcVc[] 
        UInt64[,] posLi = new UInt64[1, 1];  //index for arrays and cache startpos categories by v0 * v1

        //DATA

        public UInt64 Ns;             //number of blocks
        public UInt64 Nr;             //number of dependent records


        public Byte[] Nrr = new Byte[1]; //ns

        public string[] RespNr0 = new string[1]; //ns
        public string[] RespNr = new string[1]; //nt

        public UInt16[,] SetVar0 = new UInt16[1, 1]; //ns,vr,vc
        public UInt16[,] SetVar = new UInt16[1, 1]; //nt,vr,vc
        public UInt16[,] SetVar1 = new UInt16[1, 1]; //nt,vc1


        //RECODED DATA
        public string[,] code0 = new string[1, 1];
        public string[,] code = new string[1, 1];
        public string[,] code1 = new string[1, 1];

        //PROBABILITES
        public float[,] CHIx = new float[1, 1]; //vc0 vc
        public float[,] CHIp = new float[1, 1]; //vc0 vc
        public float[,] Ratiox = new float[1, 1]; //vc0 vc





        Int32[] sPP = new Int32[1];  //15000 * 6+1 * 4+1 
        Int32[] sPPn = new Int32[1];
        public float[] sPPratio = new float[1]; //4,4,vc0 vc
        public float[] sPPChi = new float[1]; //4,4,vc0 vc


        public float[,] ePPratio = new float[1, 1];

        public UInt16[,] SelectVar = new UInt16[1, 1]; //vc0 vc
        public UInt16[] SelectVar1 = new UInt16[1]; //vc1

        public float[] score = new float[1];
        public byte[] Hscore = new byte[1];

        public UInt64 blocksLinkedN;


        //PARAMETERS
        public int CriteriumHandling;
        public double ThresHoldPercentageCriterium;
        public double ThresHoldCriterium;
        public double DifferenceCriterium;
        public int AutoSelection;
        public int Iterations;
        public int Linkmethod;
        public int IterNumber;
        //public int LikelihoodSource;
        public int MissingsInclude;

        //OUTPUT LIKELIHOODS

        public int chi2WithinShow;
        public int Chi2BetweenShow;
        public int SelectedIdentifiersShow;
        public int CategoryFrequenciesShow;
        public int GraphicShow;
        //OUTPUT LINKAGE

        public int DistributionLikelihoodsShow;
        public int DistributionLinkedRecordsNShow;
        public int LinkedSetsShow;


        public string arraysIniGeneric(UInt16 Vc0, UInt16 Vc, UInt16 Vc1)
        {

            //RECODED DATA
            code0 = new string[Vc0 + 1, maxCat];
            code = new string[Vc + 1, maxCat];
            code1 = new string[Vc1 + 1, maxCat];

            freq0 = new UInt64[Vc0, maxCat];
            freq = new UInt64[Vc, maxCat];
            freq1 = new UInt64[Vc1, maxCat];

            //category counts
            NcVc0 = new UInt16[Vc0]; //vc0
            NcVc = new UInt16[Vc]; //vc
            NcVc1 = new UInt16[Vc1];

            //index v0 * v

            maxVnow = 0;
            if (Vc0 > Vc + Vc1) { maxVnow = Vc0; } else { maxVnow = (UInt16)(Vc + Vc1); }

            maxVnowS = 0;
            if (Vc0 > Vc) { maxVnowS = Vc0; } else { maxVnowS = Vc; }


            totVnow = (UInt16)(Vc0 + Vc + Vc1 + 1);

            posLi = new UInt64[maxVnowS, maxVnowS];



            //selections

            SelectVar = new UInt16[maxVnow, maxVnow]; //vc0 vc
            SelectVar1 = new UInt16[maxVnow]; //vc1

            //probabilities

            CHIx = new float[maxVnow, maxVnow]; //vc0 vc
            CHIp = new float[maxVnow, maxVnow]; //vc0 vc
            Ratiox = new float[maxVnow, maxVnow]; //vc0 vc

            //graph

            scoresN = new double[NcatGraph + 1];
            scoresV = new double[NcatGraph + 1];

            return "ok";
        }

        public string arraysIniData()
        {

            string t = "ok";
            try
            {
                Nrr = new Byte[Ns + 1];
                RespNr0 = new string[Ns + 1];
                RespNr = new string[Nr + 1];
                SetVar0 = new UInt16[Ns + 1, Vc0];
                SetVar = new UInt16[Nr + 1, Vc];
                SetVar1 = new UInt16[Nr + 1, Vc1];

                score = new float[Nr + 1];
                Hscore = new byte[Nr + 1];

                Nrr = new Byte[Ns + 1];

            }
            catch (IndexOutOfRangeException e)
            {
                t = "\n Cannot create data arrays. error:" + e.Message;
            }
            return t;
        }

        public UInt64[] countTotalCategories()
        {

            NcVc0H = 0; NcVcH = 0; NcVc1H = 0;
            for (UInt16 v0 = 0; v0 < Vc0; v0++) { if (NcVc0[v0] > NcVc0H) { NcVc0H = NcVc0[v0]; } }
            for (UInt16 v = 0; v < Vc; v++) { if (NcVc[v] > NcVcH) { NcVcH = NcVc[v]; } }
            for (UInt16 v1 = 0; v1 < Vc1; v1++) { if (NcVc1[v1] > NcVc1H) { NcVc1H = NcVc1[v1]; } }

            UInt64 cn = 0; UInt64 cn_old = 0;

            for (UInt16 v0 = 0; v0 < Vc0; v0++)
            {
                for (UInt16 v = 0; v < Vc; v++)
                {
                    cn += cn_old;
                    posLi[v0, v] = cn;
                    cn_old = Convert.ToUInt64(NcVc0[v0] * NcVc[v]);  //because not starting at zero!!
                }
            }
            cn += cn_old;
            UInt64[] result = new UInt64[2];
            result[0] = cn;
            result[1] = NcVc1H;
            crossN = cn;
            return result;

        }

        public string arraysIniLinkage(UInt64 catN, UInt64 cat1N)
        {
            string t = "ok";

            try
            {
                sPP = new Int32[catN];
                sPPn = new Int32[catN];
                sPPratio = new float[catN]; //4,4,vc0 vc
                sPPChi = new float[catN];

                ePPratio = new float[cat1N, Vc1];
            }
            catch (IndexOutOfRangeException e)
            {
                t = "\n Cannot create probabilities arrays. error:" + e.Message;

            }
            return t;

        }

        public string SubReportTitle(string Titel)
        {

            String Day = DateTime.Now.Day.ToString();
            String Month = DateTime.Now.Month.ToString();
            String Year = DateTime.Now.Year.ToString();
            String Hour = DateTime.Now.Hour.ToString();
            String Minute = DateTime.Now.Minute.ToString();


            String t = "SUB REPORT ; " + Titel;
            t += "   DATE & TIME ; " + Month + "-" + Day + "-" + Year + " " + Hour + "-" + Minute;


            //
            //outfrm.out.SelStart = Len(outfrm.out.text)
            return t;
        }




        public void MsgBox(string remark) { }






        public void getCriteria()
        {

            string[] co = new string[9];

            if (co[1] == "reporttitle")
            {
                SubReportID = SubReportTitle(co[2]);
            }

            if (co[1] == "missinfinclude")
            {
                MissingsInclude = 1;
            }

            if (co[1] == "linkvariables")
            {

                if (co[2] == "auto")
                {

                    AutoSelection = 1;

                }
                if (co[2] == "manual")
                {

                    AutoSelection = 0;

                }
            }

            if (co[1] == "linkmethod")
            {
                if (co[2] == "bayesian")
                {
                    Linkmethod = 0;
                }
                if (co[2] == "chisquare")
                {
                    Linkmethod = 1;
                }

            }

            if (co[1] == "linkcriterium")
            {
                if (co[2] == "bestfit")
                {
                    CriteriumHandling = 1;
                }
                if (co[2] == "bestfitabovethreshold")
                {
                    CriteriumHandling = 2;
                }
                if (co[2] == "fitsabovethreshold") //without difference criterium!
                {
                    CriteriumHandling = 3;
                }
            }

            if (co[1] == "thresholdcriterium")
            {
                if (co[2] == "auto")
                {
                    ThresHoldCriterium = -99;
                }
                if (co[2] == "manual")
                {
                    ThresHoldCriterium = Convert.ToDouble(co[3]);
                }
            }

            if (co[1] == "differencecriterium")
            {
                if (co[2] == "auto")
                {
                    DifferenceCriterium = -99;
                    if (co[2] == "manual")
                    {
                        DifferenceCriterium = Convert.ToDouble(co[3]);
                    }
                }
            }
            if (co[1] == "iterate")
            {
                if (co[2] == "manual")
                {
                    Iterations = Convert.ToInt16(co[3]);

                }
                if (co[2] == "auto")
                {


                }
            }


        }


        public void getCriteriaDefaults()
        {

            DataDir = "c:";
            BinDir = "c:";
            ResultDir = "c:";

            SubReportID = "";

            //PARAMETERS START CLASSIFICATION

            AutoSelection = 0;
            ThresHoldPercentageCriterium = 90;
            MissingsInclude = 1;

            //PARAMETERS

            CriteriumHandling = 3;
            ThresHoldCriterium = -99;
            DifferenceCriterium = -99;
            AutoSelection = 0;
            Iterations = 3;
            Linkmethod = 0;

            //OUTPUT
            chi2WithinShow = 0;
            Chi2BetweenShow = 1;

            SelectedIdentifiersShow = 1;
            CategoryFrequenciesShow = 0;
            DistributionLikelihoodsShow = 0;
            DistributionLinkedRecordsNShow = 1;
            LinkedSetsShow = 0;


        }

        public void StandardsSave(string standardsFile)
        {   //         "C:\\standard.std";  



            try
            {
                BinaryWriter bw = new BinaryWriter(new FileStream(standardsFile,
                 FileMode.Create));
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message + "\n Cannot create file.");
                return;
            }
            try
            {

                BinaryWriter bw = new BinaryWriter(new FileStream(standardsFile,
                 FileMode.Create));

                bw.Write(Vc0);
                bw.Write(DataDir);
                bw.Write(BinDir);
                bw.Write(ResultDir);

                // PARAMETERS START CLASSIFICATION

                bw.Write(AutoSelection);
                bw.Write(ThresHoldPercentageCriterium);
                bw.Write(MissingsInclude);

                // PARAMETERS

                bw.Write(CriteriumHandling);
                bw.Write(ThresHoldCriterium);
                bw.Write(DifferenceCriterium);
                bw.Write(AutoSelection);
                bw.Write(Iterations);
                bw.Write(Linkmethod);

                // OUTPUT

                bw.Write(chi2WithinShow);
                bw.Write(Chi2BetweenShow);

                bw.Write(SelectedIdentifiersShow);
                bw.Write(CategoryFrequenciesShow);
                bw.Write(DistributionLikelihoodsShow);
                bw.Write(DistributionLinkedRecordsNShow);
                bw.Write(LinkedSetsShow);


                bw.Write(GraphicShow);

                bw.Write(Delimeter);
                bw.Close();
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message + "\n Cannot read from file.");
                return;
            }
        }

        public void StandardsGet(string standardsFile)
        {

            try
            {
                BinaryReader br = new BinaryReader(new FileStream(standardsFile, FileMode.Open));
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message + "\n Cannot create file.");
                return;
            }
            try
            {
                BinaryReader br = new BinaryReader(new FileStream(standardsFile, FileMode.Open));
                //        i = br.ReadInt32();        
                //        d = br.ReadDouble();            
                //        b = br.ReadBoolean();             
                //        s = br.ReadString();


                //if ((test != 24682468))
                //{
                //    Defaults();
                //    StandardsSave("C:\\standard.std");
                //    return;
                //}

                DataDir = br.ReadString();
                BinDir = br.ReadString();
                ResultDir = br.ReadString();

                // PARAMETERS START CLASSIFICATION

                AutoSelection = br.ReadInt32();
                ThresHoldPercentageCriterium = br.ReadInt32();
                MissingsInclude = br.ReadInt32();

                // PARAMETERS

                CriteriumHandling = br.ReadInt32();
                ThresHoldCriterium = br.ReadInt32();
                DifferenceCriterium = br.ReadInt32();
                AutoSelection = br.ReadInt32();
                Iterations = br.ReadInt32();
                Linkmethod = br.ReadInt32();

                // OUTPUT

                chi2WithinShow = br.ReadInt32();
                Chi2BetweenShow = br.ReadInt32();

                SelectedIdentifiersShow = br.ReadInt32();
                CategoryFrequenciesShow = br.ReadInt32();
                DistributionLikelihoodsShow = br.ReadInt32();
                DistributionLinkedRecordsNShow = br.ReadInt32();
                LinkedSetsShow = br.ReadInt32();

                GraphicShow = br.ReadInt32();

                Delimeter = br.ReadChar();

                br.Close();
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message + "\n Cannot read from file.");
                return;
            }


        }

        public void SetsRandom(UInt64 NsIn, UInt16 Vin, UInt16 Cin)
        {

            Random rnd = new Random();

            Ns = NsIn;
            if (Ns < 500) { Ns = 500; }

            //ReDim rr(Ns)
            //ReDim Pr(Ns)
            //ReDim Hscore(1 To Ns) 'As Integer




            Vc0 = Vin;
            Vc = Convert.ToUInt16(Vc0 + 1);
            Vc1 = 1;

            Nr = Ns * 5;

            UInt16[] w = new UInt16[maxVnow];


            Var0Labels = new string[Vc0];
            VarLabels = new string[Vc];
            Var1Labels = new string[Vc1 + 1];

            NcVc0 = new UInt16[Vc0];
            NcVc = new UInt16[Vc];
            NcVc1 = new UInt16[Vc1];



            w = new UInt16[Vc0 + 1];

            freq0 = new UInt64[Vc0, maxCat];
            freq = new UInt64[Vc, maxCat];
            freq1 = new UInt64[Vc1, maxCat];


            code0 = new string[Vc0 + 1, maxCat];
            code = new string[Vc + 1, maxCat];
            code1 = new string[Vc1 + 1, maxCat];

            RespNr0 = new string[Ns + 1];
            RespNr = new string[Nr + 1];

            Nrr = new Byte[Ns + 1];

            SetVar0 = new UInt16[Ns + 1, Vc0];
            SetVar = new UInt16[Nr + 1, Vc];
            SetVar1 = new UInt16[Nr + 1, Vc1];


            for (UInt16 v0 = 0; v0 < Vc0; v0++)
            {

                Var0Labels[v0] = "InDep" + v0.ToString();
                missing0[v0] = 1;
            }
            for (UInt16 v = 0; v < Vc; v++)
            {

                VarLabels[v] = "Dep" + v.ToString();
                missing[v] = 1;
            }
            for (UInt16 v1 = 0; v1 < Vc1 + 1; v1++)
            {
                Var1Labels[v1] = "Sing" + v1.ToString();
                missing1[v1] = 1;
            }

            for (UInt16 v0 = 0; v0 < Vc0; v0++)
            {
                NcVc0[v0] = Cin;
            }

            for (UInt16 v = 0; v < Vc; v++)
            {
                NcVc[v] = Convert.ToUInt16(Cin + 1);    //altijd 1 hoger !! : Convert.ToInt16(rnd.Next(1, 2))) geeft altijd 1 extra categorie.. (dan alleen 1 toevoegen)
            }
            //NcVc[Vc] = Convert.ToUInt16(NcVc0[Vc0] + 1);  //correction..
            for (UInt16 v1 = 0; v1 < Vc1; v1++)
            {
                NcVc1[v1] = 2;
            }




            UInt64 posVarsV = 0; UInt64 posVarsK = 0; UInt64 PrNow = 0;
            string respNr0Old = "";
            for (UInt64 s = 1; s < Ns + 1; s++)
            {
                //if (Math.Round(Con vert.ToSingle(s) / 1000) == Convert.ToSingle(s) / 1000) { saveStatus(statusFileName, "creating random record: " + s.ToString()); }
                byte rnNow = Convert.ToByte(rnd.Next(2, 7));
                //Pr[s] = Pr[s - 1];

                UInt16[,] SRdata = new UInt16[Vin + Vin + 1, Cin + 1];
                string[] respnrI = new string[maxRperRecordS];
                long LongNr = (long)((rnd.NextDouble() * 2.0 - 1.0) * long.MaxValue);
                respNr0Old = LongNr.ToString().Trim();

                respnrI[0] = respNr0Old; ;
                RespNr0[s] = respNr0Old;



                for (UInt16 v0 = 0; v0 < Vc0; v0++)
                {

                    w[v0] = Convert.ToUInt16(-1 + rnd.Next(1, NcVc0[v0] + 1)); // Convert.ToInt32(Rnd() * NcVc0[v0]) + 1;
                    SRdata[0, v0] = w[v0];
                }

                SRdata[1, 0] = Convert.ToUInt16(-1 + rnd.Next(1, NcVc[1] + 1));
                for (UInt16 v = 1; v < Vc; v++)
                {
                    SRdata[1, v] = Convert.ToUInt16(w[v] + 1);
                }
                for (UInt16 v1 = 0; v1 < Vc1; v1++)
                {
                    SRdata[1, Vc + v1] = Convert.ToUInt16(-1 + rnd.Next(1, NcVc1[v1] + 1));
                }


                respnrI[1] = respNr0Old;
                RespNr[PrNow + 1] = respNr0Old;




                for (byte r = 2; r < rnNow + 1; r++)
                {
                    respnrI[r] = respNr0Old; ;
                    RespNr[PrNow + r] = respNr0Old;

                    for (UInt16 v = 0; v < Vc; v++)
                    {
                        SRdata[r, v] = Convert.ToUInt16(-1 + rnd.Next(1, NcVc[v] + 1));
                    }
                    for (UInt16 v1 = 0; v1 < Vc1; v1++)
                    {
                        SRdata[r, Vc + v1] = Convert.ToUInt16(-1 + rnd.Next(1, NcVc1[v1] + 1));
                    }
                }



                for (UInt16 v0 = 0; v0 < Vc0; v0++)
                {
                    freq0[v0, SRdata[0, v0]] += 1;

                }
                for (byte r = 1; r < rnNow + 1; r++)
                {
                    for (UInt16 v = 0; v < Vc; v++)
                    {
                        freq[v, SRdata[r, v]] += 1;
                    }
                    for (UInt16 v1 = 0; (v1 < Vc1); v1++)
                    {
                        freq1[v1, SRdata[r, Vc + v1]] += 1;
                    }
                }





                saveSetToArray(s, PrNow, rnNow, SRdata);
                Nrr[s] = rnNow;


                //Pr[s+1]  =Pr[s] + rnNow;
                PrNow = PrNow + rnNow;
            }



            for (UInt16 v0 = 0; v0 < Vc0; v0++)
            {
                for (UInt16 c = 0; c < NcVc0[v0]; c++)
                {
                    code0[v0, c] = "C" + c.ToString("0");
                }
            }
            for (UInt16 v = 0; v < Vc; v++)
            {
                for (UInt16 c = 0; c < NcVc[v]; c++)
                {
                    code[v, c] = "C" + c.ToString("0");
                }
            }

            for (UInt16 v1 = 0; v1 < Vc1; v1++)
            {
                for (UInt16 c = 0; c < NcVc1[v1]; c++)
                {
                    code1[v1, c] = "C" + c.ToString("0");
                }
            }
            //frequencies();


        }

        public Boolean saveSetToArray(UInt64 sNow, UInt64 PrNow, Byte rNow, UInt16[,] SRdata)
        {

            for (UInt16 v0 = 0; v0 < Vc0; v0++)
            {
                SetVar0[sNow, v0] = SRdata[0, v0];

            }
            for (byte r = 1; r <= rNow; r++)
            {
                for (UInt16 v = 0; v < Vc; v++)
                {
                    SetVar[PrNow + r, v] = SRdata[r, v];
                }
                for (UInt16 v1 = 0; v1 < Vc1; v1++)
                {
                    SetVar1[PrNow + r, v1] = SRdata[r, Vc + v1];
                }
            }

            return true;
        }

        public StringBuilder Probabilities(byte iter_n)
        {

            Int64 PrNow = 0;

            IterNumber = iter_n;

            StringBuilder sb = new StringBuilder();
            string t = "";

            switch (Linkmethod)
            {
                case -1:
                    t = "Likelihoods (obs/exp) : Link step : "
                                + (IterNumber + 1).ToString("0", CultureInfo.InvariantCulture) + " (N = "
                                + Nsmall.ToString("0", CultureInfo.InvariantCulture) + " < -99)";
                    break;
                case 0:
                    t = "Likelihoods : Link step : "
                                + (IterNumber + 1).ToString("0", CultureInfo.InvariantCulture) + " (N = "
                                + Nsmall.ToString("0.00", CultureInfo.InvariantCulture) + " < -99)";
                    break;
                case 1:
                    t = "CHI Squares : link step : " + (IterNumber + 1).ToString("0", CultureInfo.InvariantCulture);
                    break;
            }

            t += "\r\n"; t += "\r\n";

            t += "Total number of combined categories (all by all) : " + crossN.ToString();
            t += "\r\n"; t += "\r\n";

            sb.Append(t); t = "";

            t = "Number of categories set 0: " + Delimeter;

            for (UInt16 v0 = 0; v0 < Vc0; v0++) { t += NcVc0[v0]; t += Delimeter; }
            t += "\r\n";
            t += "Number of categories set 1: " + Delimeter;
            for (UInt16 v = 0; v < Vc; v++) { t += NcVc[v]; t += Delimeter; }
            t += "\r\n";
            t += "Number of singular categories set 1: " + Delimeter;
            for (UInt16 v1 = 0; v1 < Vc1; v1++) { t += NcVc1[v1]; t += Delimeter; }
            t += "\r\n";

            sb.Append(t); t = "";



            Array.Clear(sPP, 0, sPP.Length);
            Array.Clear(sPPn, 0, sPP.Length);
            Array.Clear(sPPChi, 0, sPPChi.Length);
            Array.Clear(sPPratio, 0, sPPratio.Length);



            Array.Clear(CHIx, 0, CHIx.Length);
            Array.Clear(Ratiox, 0, Ratiox.Length);
            Array.Clear(CHIp, 0, CHIx.Length);

            Int32[,] ePP = new Int32[NcVc1H + 1, Vc1 + 1];
            Int32[,] ePPn = new Int32[NcVc1H + 1, Vc1 + 1];

            Array.Clear(ePPratio, 0, ePPratio.Length);

            Int64 N = 0;
            Int64 Nn = 0;


            float observed = 0; float notobserved = 0;
            float expected = 0;


            if (IterNumber == 0)
            {

                t = "Iteration Step 1: Likelihoods based on raw linked records";



                //  ReDim Hscore(Nr)

                //for (INt64 s = 1; s < Ns + 1; s++)
                //{
                //    for (byte r = 1; r < Nrr[s] + 1; r++)
                //    {
                //        Hscore[Pr[s] + r] = 1;
                //   }
                //}
                for (UInt16 v0 = 0; v0 < Vc0; v0++)
                {
                    for (UInt16 v = 0; v < Vc; v++)
                    {
                        SelectVar[v0, v] = 1;
                    }
                }
                for (UInt16 v1 = 0; v1 < Vc1; v1++)
                {
                    SelectVar1[v1] = 1;
                }
            }

            else
            {
                t = "Iteration Step ; " + IterNumber.ToString();
            }




            // ReDim PP(NcVc0H, NcVcH, 1 To Vc0, 1 To Vc) As Single
            // ReDim PPn(NcVc0H, NcVcH, 1 To Vc0, 1 To Vc) As Single
            // ReDim PPChi(NcVc0H, NcVcH, 1 To Vc0, 1 To Vc)
            // ReDim PPratio(NcVc0H, NcVcH, 1 To Vc0, 1 To Vc)




            //CONSTRUCT CROSSTABLES OF PP and PPn (linked and not linked)  

            for (UInt64 s = 1; s < Ns + 1; s++)
            {
                //if (Math.Round(Convert.ToSingle(s) / 1000) == Convert.ToSingle(s) / 1000) { saveStatus(statusFileName, "calculating probabilities: " + s.ToString()); }
                //Get data of one set         
                UInt16[,] SRdata = new UInt16[maxRperRecordS, maxVnow];

                for (Int16 v0 = 0; v0 < Vc0; v0++)
                {
                    SRdata[0, v0] = SetVar0[s, v0];
                }
                for (byte r = 1; r <= Nrr[s]; r++)
                {
                    for (Int16 v = 0; v < Vc; v++)
                    {
                        //SRdata[r, v] = SetVar[Pr[s] + r, v];
                        SRdata[r, v] = SetVar[PrNow + r, v];
                    }
                    for (Int16 v1 = 0; v1 < Vc1; v1++)
                    {
                        //SRdata[r, Vc + v1] = SetVar1[Pr[s] + r, Vc + v1];
                        SRdata[r, Vc + v1] = SetVar1[PrNow + r, v1];
                    }
                }



                //SET Hscore to zero if first run;
                if (IterNumber == 0)
                {

                    for (byte r = 1; r < Nrr[s] + 1; r++) { Hscore[PrNow + r] = 1; }
                }
                //Get scores of selected and non selected
                for (byte r = 1; r <= Nrr[s]; r++)
                {
                    for (UInt16 v0 = 0; v0 < Vc0; v0++)
                    {
                        for (UInt16 v = 0; v < Vc; v++)
                        {
                            if (SelectVar[v0, v] == 1)
                            {



                                UInt64 i = posLi[v0, v] + SRdata[0, v0] * (ulong)NcVc[v] + SRdata[r, v];
                                if (Hscore[PrNow + r] == 1)
                                {
                                    sPP[i] += 1;
                                }
                                if (Hscore[PrNow + r] == 0)
                                {
                                    sPPn[i] += 1;
                                }




                            }

                        }
                    }
                    for (UInt16 v1 = 0; v1 < Vc1; v1++)
                    {
                        if (SelectVar1[v1] > 0)
                        {

                            if (Hscore[PrNow + r] == 1) { ePP[SRdata[r, Vc + v1], v1] += 1; }
                            if (Hscore[PrNow + r] == 0) { ePPn[SRdata[r, Vc + v1], v1] += 1; }



                        }
                    }
                }

                PrNow += Nrr[s];
            }
            for (UInt16 v0 = 0; v0 < Vc0; v0++)
            {
                for (UInt16 v = 0; v < Vc; v++)
                {
                    if (SelectVar[v0, v] == 1)
                    {
                        //COMPUTE
                        N = 0; Nn = 0;
                        Int64[] nr = new Int64[NcVc0[v0] + 1];
                        Int64[] nc = new Int64[NcVc[v] + 1];
                        //double[] Nnr = new double[NcVc0[v0] + 1];
                        //double[] Nnc = new double[NcVc[v] + 1];

                        for (UInt16 c0 = 0; c0 < NcVc0[v0]; c0++)
                        {
                            for (UInt16 c = 0; c < NcVc[v]; c++)
                            {



                                UInt64 i = posLi[v0, v] + (ulong)c0 * NcVc[v] + c;

                                N += sPP[i];
                                Nn += sPPn[i];

                                nr[c0] += sPP[i];
                                nc[c] += sPP[i];


                            }
                        }
                        if (Nn == 0)
                        {
                            Nn = 1;
                        }
                        //COMPUTE CHI en RATIO
                        Ratiox[v0, v] = 1;
                        for (UInt16 c0 = 0; c0 < NcVc0[v0]; c0++)
                        {
                            for (UInt16 c = 0; c < NcVc[v]; c++)
                            {
                                Single ppc = 0; ; Single ppr = 1;




                                UInt64 i = posLi[v0, v] + (ulong)c0 * NcVc[v] + c;
                                observed = sPP[i];
                                notobserved = sPPn[i];


                                if (notobserved == 0)
                                {
                                    notobserved = 1;
                                }
                                if (N > 0)
                                {
                                    expected = Convert.ToSingle((nr[c0] * nc[c]) / N);
                                }
                                if (expected > 0)
                                {
                                    ppc = Convert.ToSingle(Math.Pow((observed - expected), 2) / expected * Math.Sign(observed - expected));
                                    CHIx[v0, v] += ppc;
                                }
                                if (expected > 0 && observed > 0)
                                {

                                    ppr = observed / expected; Ratiox[v0, v] *= ppr;
                                    if (IterNumber > 0 && Linkmethod == 0)
                                    {
                                        ppr = (observed / N) / (notobserved / Nn);

                                    }
                                }
                                else
                                {
                                    ppr = 1;
                                }


                                i = posLi[v0, v] + (ulong)c0 * NcVc[v] + c;
                                sPPChi[i] = ppc;
                                sPPratio[i] = ppr;



                            }
                        }


                        //SELECT DEPENDENCY OR NOT

                        UInt16 degreesOfFreedom;

                        UInt16 f1;
                        UInt16 f2;

                        f1 = (UInt16)(NcVc0[v0] - 1);
                        f2 = (UInt16)(NcVc[v] - 1);

                        if (f1 * f2 > 10000)
                        {
                            degreesOfFreedom = 10000;
                        }
                        else
                        {
                            degreesOfFreedom = (UInt16)((NcVc0[v0] - 1) * (NcVc[v] - 1));
                        }
                        //                   CHI(1, vrijheid, CHIx[v0, v], CHIp[v0, v]);


                    }
                }
            }

            for (UInt16 v1 = 0; v1 < Vc1; v1++)
            {
                if (SelectVar1[v1] > 0)
                {

                    N = 0; Nn = 0;

                    for (UInt16 c1 = 0; c1 < NcVc1[v1]; c1++)
                    {

                        N += ePP[c1, v1];
                        Nn += ePPn[c1, v1];



                    }

                    if (Linkmethod == 0)
                    {
                        for (UInt16 c1 = 0; c1 < NcVc1[v1]; c1++)
                        {

                            if (IterNumber > 0)
                            {


                                observed = ePP[c1, v1]; notobserved = ePPn[c1, v1];

                                if (observed > 0 && N > 0)
                                {
                                    ePPratio[c1, v1] = (observed / N) / (notobserved / Nn);
                                    //ePPratio[c1, v1] = Convert.ToSingle((observed / N) / (notobserved / Nn));
                                }
                            }

                        }
                    }


                }
            }

            if (AutoSelection == 1)
            {
                AutoSelectVariables();
            }
            else
            {
                if (IterNumber == 0)
                {
                    //   ReDim SelectVar(Vc0, Vc)
                    //   ReDim SelectVar1(Vc1)
                    for (UInt16 v0 = 0; v0 < Vc0; v0++)
                    {
                        for (UInt16 v = 0; v < Vc; v++)
                        {

                            SelectVar[v0, v] = 1;
                        }
                    }
                    for (UInt16 v1 = 0; v1 < Vc1; v1++)
                    {
                        SelectVar1[v1] = 1;
                    }
                }
            }



            Array.Clear(Hscore, 0, Hscore.Length);

            //SHOW CHI2 TABLE within VARIABLES
            if ((Vc + Vc0 + Vc1) * (NcVc0H + NcVcH + NcVc1H) > 1000) { chi2WithinShow = 0; }
            if (chi2WithinShow == 1)
            {
                t = "";
                t += "N Within";
                t += "\r\n";
                sb.Append(t); t = "";
                for (UInt16 v0 = 0; v0 < Vc0; v0++)
                {

                    t += "Independent ; " + Var0Labels[v0];
                    for (UInt16 c0 = 0; c0 < NcVc0[v0]; c0++)
                    {
                        t += ",";
                    }
                    t += ",,";
                }
                t += "\r\n";
                sb.Append(t); t = ",";
                for (UInt16 v0 = 0; v0 < Vc0; v0++)
                {
                    t += ",";
                    for (UInt16 c0 = 0; c0 < NcVc0[v0]; c0++)
                    {
                        t += "c" + c0.ToString() + "(" + code0[v0, c0] + ")";
                        t += ",";
                    }
                    t += ",";
                }
                t += "\r\n";
                sb.Append(t); t = "";


                for (UInt16 v = 0; v < Vc; v++)
                {
                    for (UInt16 c = 0; c < NcVc[v]; c++)
                    {
                        for (UInt16 v0 = 0; v0 < Vc0; v0++)
                        {
                            t += "dependent ; " + VarLabels[v];
                            t += ",";
                            t += "c" + c.ToString() + "(" + code[v, c] + ")";
                            t += ",";
                            for (UInt16 c0 = 0; c0 < NcVc0[v0]; c0++)
                            {


                                UInt64 i = posLi[v0, v] + (ulong)c0 * NcVc[v] + c;
                                t += sPP[i].ToString("0", CultureInfo.InvariantCulture);


                                t += ",";
                            }
                        }
                        t += "\r\n";
                        sb.Append(t); t = "";
                    }
                }
                t += "\r\n"; sb.Append(t); t = "";



                t = "";
                if (Linkmethod == 0) { t += "Likelihood Ratio's Within"; }
                if (Linkmethod == 1) { t += "Chi Square Within"; } // ; Independent ; " + Var0Labels[v0] + "&& dependent ; " + VarLabels[v] + " ; p = " + (CHIp[v0, v]).ToString("0.00", CultureInfo.InvariantCulture) + "}";
                t += "\r\n";
                sb.Append(t); t = "";
                for (UInt16 v0 = 0; v0 < Vc0; v0++)
                {

                    t += "Independent ; " + Var0Labels[v0];
                    for (UInt16 c0 = 0; c0 < NcVc0[v0]; c0++)
                    {
                        t += ",";
                    }
                    t += ",,";
                }
                t += "\r\n";
                sb.Append(t); t = ",";
                for (UInt16 v0 = 0; v0 < Vc0; v0++)
                {
                    t += ",";
                    for (UInt16 c0 = 0; c0 < NcVc0[v0]; c0++)
                    {
                        t += "c" + c0.ToString() + "(" + code0[v0, c0] + ")";
                        t += ",";
                    }
                    t += ",";
                }
                t += "\r\n";
                sb.Append(t); t = "";


                for (UInt16 v = 0; v < Vc; v++)
                {
                    for (UInt16 c = 0; c < NcVc[v]; c++)
                    {
                        for (UInt16 v0 = 0; v0 < Vc0; v0++)
                        {
                            t += "dependent ; " + VarLabels[v];
                            t += ",";
                            t += "c" + c.ToString() + "(" + code[v, c] + ")";
                            t += ",";
                            for (UInt16 c0 = 0; c0 < NcVc0[v0]; c0++)
                            {


                                UInt64 i = posLi[v0, v] + (ulong)c0 * NcVc[v] + c;
                                if (Linkmethod == 0) { t += sPPratio[i].ToString("0.00", CultureInfo.InvariantCulture); }
                                if (Linkmethod == 1) { t += sPPChi[i].ToString("0.00", CultureInfo.InvariantCulture); }


                                t += ",";
                            }
                        }
                        t += "\r\n";
                        sb.Append(t); t = "";
                    }
                }
                if (Linkmethod == 0)
                {
                    if (Linkmethod == 0) { t = "\r\n"; ; t += "Likelihood Ratio's Within"; }
                    t += "\r\n";

                    for (UInt16 v1 = 0; (v1 < Vc1); v1++)
                    {
                        t += "variable ; " + Var1Labels[v1]; t += ",";
                        for (UInt16 c1 = 0; (c1 < NcVc1[v1]); c1++)
                        {
                            t += "c" + c1.ToString() + "(" + code1[v1, c1] + ")"; t += ",";
                        }
                        t += "\r\n";
                        for (UInt16 c = 0; (c < NcVc1[v1]); c++)
                        {
                            t += ePPratio[c, v1].ToString("0.00", CultureInfo.InvariantCulture); t += ",";
                        }
                        t += "\r\n";
                    }
                }
                t += "\r\n"; sb.Append(t); t = "";

            }

            if (Chi2BetweenShow == 1)
            {
                inform("");
                t = "";
                if (Linkmethod == 0) { t += "Ratio''s between Dependent and Independ Identifiers"; };
                if (Linkmethod == 1) { t += "Chi Squares between Dependent and Independ Identifiers"; };
                t += ",";
                t += "Dependent";
                t += "\r\n";
                sb.Append(t); t = ",";
                for (UInt16 v = 0; v < Vc; v++)
                {
                    t += "dep " + v.ToString("0", CultureInfo.InvariantCulture) + ": (" + VarLabels[v] + ")";
                    t += ",";
                }
                t += "\r\n";
                sb.Append(t); t = "";
                for (UInt16 v0 = 0; v0 < Vc0; v0++)
                {
                    t += "dep " + v0.ToString("0", CultureInfo.InvariantCulture) + ": (" + Var0Labels[v0] + ")";
                    for (UInt16 v = 0; v < Vc; v++)
                    {
                        t += ",";
                        if (Linkmethod == 0) { t += Ratiox[v0, v].ToString("0.00", CultureInfo.InvariantCulture); }
                        if (Linkmethod == 1)
                        {
                            t += CHIx[v0, v].ToString("0.00", CultureInfo.InvariantCulture);
                            if (CHIp[v0, v] < 0.05 && CHIp[v0, v] != 0)
                            {
                                if (CHIp[v0, v] < 0.001) { t += "**"; } else { t += "*"; }
                            }
                        }
                    }
                    t += "\r\n";
                    sb.Append(t); t = "";
                }
                t += "\r\n"; sb.Append(t); t = "";
            }
            // ReDim PP(0, 0, 0, 0)


            return sb;

        }


        public dataCheckResults SetsGetDelimetedCountAndCheck(System.IO.StreamReader file)
        {
             
            dataCheckResults result = new dataCheckResults();

            char dChar = Delimeter;
            UInt16 pos = 0; UInt16 rpos = 0;
            string[] words = new string[1];
            try
            {
                t = ""; rpos = 0;
                do
                {
                    t = file.ReadLine(); pos += 1;
                    //t = data[rpos]; rpos += 1;
                } while (t.Trim() == "");

                words = t.Split(dChar);
                //Ns = Convert.ToUInt64(words[0]);
                Vc0 = Convert.ToUInt16(words[0]);
                Vc = Convert.ToUInt16(words[1]);
                Vc1 = Convert.ToUInt16(words[2]);

                Var0Labels = new string[Vc0];
                VarLabels = new string[Vc];
                Var1Labels = new string[Vc1];


            }
            catch (Exception e) {

                result.result = "expected 3 numbers describing the number of indepentdent, dependent and singular vairiables with delimeter: " + dChar.ToString();  
                result.result += "  (" + e.Message + ")";
                result.error = true;
                return result;
            }
         


            UInt16 i = 0; UInt64 r = 0; t = "";

            try
            {
                do
            {
                do { t = file.ReadLine(); pos += 1; } while (t.Trim() == "");


                 words = t.Split(dChar);
                Var0Labels[i] = words[0];
                i += 1;
                if (t.Length > 1) { missingS0[i] = words[1].Trim(); }
            } while (i < Vc0);
            i = 0;
            do
            {
                do { t = file.ReadLine(); pos += 1; } while (t.Trim() == "");


                 words = t.Split(dChar);
                VarLabels[i] = words[0];
                if (t.Length > 1) { missingS[i] = words[1].Trim(); }
                i += 1;
            } while (i < Vc);
            i = 0;

            if (Vc1 > 0)
            {
                do
                {
                    do { t = file.ReadLine(); pos += 1; } while (t.Trim() == "");


                      words = t.Split(dChar);
                    Var1Labels[i] = words[0];
                    if (t.Length > 1) { missingS1[i] = words[1].Trim(); }
                    i += 1;
                } while (i < Vc1);
            }
            rpos = pos;


            }
            catch (Exception e)
            {

                result.result = "expected " + Vc0.ToString() +" labels for independent fields, " + Vc.ToString() + " labels for dependent fields and " + Vc1.ToString() + " labels for singular fields with delimeter: "  + dChar.ToString();
                result.result += "  (" + e.Message + ")";
                result.error = true;
                return result;
            }
 



            Int16[,] SRdata = new Int16[maxRperRecordS, maxVnow];

            string[,] SetVarI = new string[maxRperRecordS, maxVnow];

            string[] respnrI = new string[maxRperRecordS];

            UInt64 s = 0; byte rnNow = 0;
            string RecordNr = "";
            i = 0;

            try
            {

               
            do
            {

                do { t = file.ReadLine(); if (t == null) { break; } } while (t.Trim() == "");
                if (t != null) { words = t.Split(dChar); }
                if (RecordNr == words[0].Trim() || t == null) { i += 1; } else { i = 0; }
                if (i == 0)
                {

                    if (s > 0) { }//SRdata = recodeVars(rnNow, SetVarI); }
                                  //SetVarI = new string[maxRperRecordS, maxVnow];
                    s += 1; rnNow = 0;
                    //if (Math.Round(Convert.ToSingle(s) / 1000) == Convert.ToSingle(s) / 1000) { saveStatus(statusFileName, "reading block: " + s.ToString()); }
                    RecordNr = words[0].Trim();


                }
                if (i > 0 & i < maxRperRecordS)
                {
                    r += 1; rnNow += 1;
                    //RespNr[s] = words[1].Trim();  //RespNr[(Pr[s] + r)]
                    //RespNr[r] = words[1].Trim();



                }
                else
                {
                    //just omit theese (should give a warning to!!;
                }



            } while (t != null);
            }
            catch (Exception e)
            {

                result.result = "datafile with delimeter: " + dChar.ToString() + " in wrong format given the number of fields defined in the header. Did you define the fields in the right way?";
                result.result += "  (" + e.Message + ")";
                result.error = true;
                return result;
            }

            Ns = s; Nr = r;


            result.Ns = Ns;
            result.Nr = Nr;
            result.Vc0 = Vc0;
            result.Vc = Vc;
            result.Vc1 = Vc1;
            result.rpos = rpos;
            result.result = "data Ok!";

            file.Close();
            return result;
            }

        public dataReadResults SetsGetDelimetedReadAllData(System.IO.StreamReader file,    UInt64 rpos)

        {
            char dChar = Delimeter;
            dataReadResults result = new dataReadResults();

            UInt64 pos = 0; UInt64 posVarsV = 0; UInt64 posVarsK = 0;


            Array.Clear(NcVc0, 0, NcVc0.Length);
            Array.Clear(NcVc, 0, NcVc.Length);
            Array.Clear(NcVc1, 0, NcVc1.Length);

            Array.Clear(code0, 0, code0.Length);
            Array.Clear(code, 0, code.Length);
            Array.Clear(code1, 0, code1.Length);

            UInt16[,] SRdata = new UInt16[Nr, maxVnow];
            string[] words = new string[maxVnow];


            do { t = file.ReadLine(); pos += 1; } while (pos != rpos);
            string[,] SetVarI = new string[maxRperRecordS, maxVnow];
            string[] respnrI = new string[maxRperRecordS];
            UInt64 s = 0; byte rnNow = 0;
            string RecordNr = "";
            UInt64 i = 0; UInt64 r = 0; t = "";
            UInt64 PrNow = 0; UInt64 PrNowOld = 0;

            do
            {

                do { t = file.ReadLine(); if (t == null) { break; } } while (t.Trim() == "");
                if (t != null) { words = t.Split(dChar); }
                else
                {

                    SRdata = recodeVars(rnNow, SetVarI);
                    PrNow = PrNowOld; PrNowOld = r;
                    Frequencies(rnNow, SRdata);

                    saveSetToArray(s, PrNow, rnNow, SRdata); Nrr[s] = rnNow;

                }

                if (RecordNr == words[0].Trim()) { i += 1; } else { i = 0; }
                if (i == 0)
                {
                    if (s > 0)
                    {

                        SRdata = recodeVars(rnNow, SetVarI);
                        //Pr[s+1] = r;
                        PrNow = PrNowOld; PrNowOld = r;
                        Frequencies(rnNow, SRdata);
                        saveSetToArray(s, PrNow, rnNow, SRdata); Nrr[s] = rnNow;

                    }

                    SetVarI = new string[maxRperRecordS, maxVnow];

                    s += 1; rnNow = 0;
                    RecordNr = words[0].Trim();


                    RespNr0[s] = words[1].Trim();
                    respnrI[0] = words[1].Trim();

                    for (UInt16 v0 = 0; v0 < Vc0; v0++)
                    {
                        SetVarI[0, v0] = words[2 + v0];
                    }

                }
                if (i > 0 & i < maxRperRecordS)
                {

                    r += 1; rnNow += 1;
                    RespNr[r] = words[1].Trim();
                    respnrI[i] = words[1].Trim();

                    for (UInt16 v = 0; v < Vc; v++)
                    {

                        SetVarI[rnNow, v] = words[2 + v];
                    }
                    for (UInt16 v1 = 0; v1 < Vc1; v1++)
                    {

                        SetVarI[rnNow, Vc + v1] = words[2 + Vc + v1];
                    }

                }



            } while (t != null);


            Ns = s; Nr = r;
            //frequencies();



            //  Close #3



            //inform("");

            result.Ns = Ns;
            result.Nr = Nr;
            result.result = "reading data Ok!";

            return result;
        }

        public void SetsWriteDelimeted(System.IO.StreamWriter file, string dChar, bool combine)
        {

            //System.IO.StreamWriter file = new System.IO.StreamWriter(rawfileNow);

            //FILE INFO
            if (!combine)
            {
                t = Vc0.ToString() + dChar + Vc.ToString() + dChar + Vc1.ToString(); file.WriteLine(t);
                t = ""; file.WriteLine(t);
                for (UInt16 i = 0; i < Vc0; i++) { t = Var0Labels[i] + dChar + missingS0[i]; file.WriteLine(t); }
                t = ""; file.WriteLine(t);
                for (UInt16 i = 0; i < Vc; i++) { t = VarLabels[i] + dChar + missingS[i]; file.WriteLine(t); }
                t = ""; file.WriteLine(t);
                for (UInt16 i = 0; i < Vc1; i++) { t = Var1Labels[i] + dChar + missingS1[i]; file.WriteLine(t); }
            }

            //DATA
            t = ""; file.WriteLine(t);

            Int64 PrNow = 0;
            for (UInt64 s = 1; s <= Ns; s++)
            {
                t = s.ToString() + dChar;
                t += RespNr0[s].ToString() + dChar.ToString(); ;
                for (UInt16 v0 = 0; v0 < Vc0; v0++) { t += code0[v0, SetVar0[s, v0]] + dChar.ToString(); ; }
                file.WriteLine(t);
                for (Byte r = 1; r <= Nrr[s]; r++)
                {
                    t = s.ToString() + dChar.ToString(); ;
                    t += RespNr[PrNow + r].ToString() + dChar.ToString(); ;
                    //for (UInt16 k = 0; k < Kc; k++) { t += "sk" + k.ToString() + dChar; }
                    for (UInt16 v = 0; v < Vc; v++) { t += code[v, SetVar[PrNow + r, v]] + dChar.ToString(); }
                    for (UInt16 v1 = 0; v1 < Vc1; v1++) { t += code1[v1, SetVar1[PrNow + r, v1]] + dChar.ToString(); }
                    file.WriteLine(t);
                }
                t = ""; file.WriteLine(t);
                PrNow += Nrr[s];
            }


            //file.Close();

        }

        public void Frequencies(byte RecordN, UInt16[,] SRdata)
        {
            for (UInt16 v0 = 0; v0 < Vc0; v0++)
            {
                freq0[v0, SRdata[0, v0]] += 1;

            }
            for (byte r = 1; r < RecordN + 1; r++)
            {
                for (UInt16 v = 0; v < Vc; v++)
                {
                    freq[v, SRdata[r, v]] += 1;
                }
                for (UInt16 v1 = 0; (v1 < Vc1); v1++)
                {
                    freq1[v1, SRdata[r, Vc + v1]] += 1;
                }
            }
        }

        public UInt16[,] recodeVars(byte RecordN, string[,] SetVarI)
        {

            UInt16[,] SRdata = new UInt16[maxRperRecordS, maxVnow];

            UInt16 found;
            for (UInt16 v0 = 0; v0 < Vc0; v0++)
            {
                found = maxCat;

                for (UInt16 c0 = 0; (c0 < NcVc0[v0]); c0++)
                {
                    if ((SetVarI[0, v0] == code0[v0, c0])) { found = c0; break; }
                }
                if (found == maxCat)
                {

                    found = NcVc0[v0];
                    //if (NcVc0[v0] > Int16.MaxValue) { MsgBox(("to many categories : variable " + Var0Labels[v0])); return; }
                    code0[v0, NcVc0[v0]] = SetVarI[0, v0];
                    NcVc0[v0] += 1;
                }

                SRdata[0, v0] = found;

            }
            for (byte r = 1; r <= RecordN; r++)
            {
                for (UInt16 v = 0; v < Vc; v++)
                {
                    found = maxCat;

                    for (UInt16 c = 0; (c < NcVc[v]); c++)
                    {
                        if ((SetVarI[r, v] == code[v, c]))
                        {
                            found = c;
                            break;
                        }
                    }
                    if (found == maxCat)
                    {

                        found = NcVc[v];
                        //if (NcVc[v] > maxCat) { MsgBox(("to many categories : variable " + VarLabels[v])); return; }
                        code[v, NcVc[v]] = SetVarI[r, v];
                        NcVc[v] += 1;

                    }

                    SRdata[r, v] = found;

                }
                for (UInt16 v1 = 0; v1 < Vc1; v1++)
                {
                    found = maxCat;

                    for (UInt16 c1 = 0; (c1 < NcVc1[v1]); c1++)
                    {
                        if (SetVarI[r, Vc + v1] == code1[v1, c1]) { found = c1; break; }
                    }
                    if (found == maxCat)
                    {

                        found = NcVc1[v1];
                        //if (NcVc1[v1] > Int16.MaxValue) { MsgBox(("to many categories : variable " + Var1Labels[v1])); return; }
                        code1[v1, NcVc1[v1]] = SetVarI[r, Vc + v1];
                        NcVc1[v1] += 1;

                    }

                    SRdata[r, Vc + v1] = found;
                }

            }

            return SRdata;
        }


        public StringBuilder OutputFileInfo(string FileName)
        {
            StringBuilder sb = new StringBuilder();

            if (FileName.Trim() == "") { FileName = "Randomly created data"; }
            t = "REPORT BASED ON FILE : " + FileName;
            t += "\r\n"; sb.Append(t); t = "";
            t = "Independent Record N : " + Ns.ToString("0", CultureInfo.InvariantCulture);
            t += "\r\n"; sb.Append(t); t = "";
            t = "Dependent Record N : " + Nr.ToString("0", CultureInfo.InvariantCulture);
            t += "\r\n"; sb.Append(t); t = "";
            t = "Independent Variables";

            for (UInt16 v0 = 0; (v0 < Vc0); v0++)
            {
                t += ",";
                t += v0.ToString("0", CultureInfo.InvariantCulture) + " : " + Var0Labels[v0];
            }
            t += "\r\n"; sb.Append(t); t = "";
            t = "Dependent Variables";

            for (UInt16 v = 0; (v < Vc); v++)
            {
                t += ",";
                t += v.ToString("0", CultureInfo.InvariantCulture) + " : " + VarLabels[v];
            }
            t += "\r\n"; sb.Append(t); t = "";
            t = "Singular Variables";
            if ((Vc1 == 0))
            {
                t += ",";
                t += "None";
            }
            for (UInt16 v1 = 0; (v1 < Vc1); v1++)
            {
                t += ",";
                t += v1.ToString("0", CultureInfo.InvariantCulture) + " : " + Var1Labels[v1];
            }
            t += "\r\n"; t += "\r\n";
            sb.Append(t); t = "";
            return sb;

        }

        public StringBuilder OutputFrequencies()
        {

            StringBuilder sb = new StringBuilder();

            string t = "Frequencies";
            sb.AppendLine(t);
            for (UInt16 v0 = 0; (v0 < Vc0); v0++)
            {
                t = "Independent Variable : " + Var0Labels[v0].Trim();
                t += "\r\n"; sb.Append(t); t = "";
                t = "New code";
                t += ",";
                t += "Old code";
                t += ",";
                t += "Category N";
                t += "\r\n"; sb.Append(t); t = "";

                for (UInt16 c0 = 0; (c0 < NcVc0[v0]); c0++)
                {
                    t = c0.ToString("0", CultureInfo.InvariantCulture);
                    t += ",";
                    t += code0[v0, c0].Trim();
                    t += ",";
                    t += freq0[v0, c0].ToString("0", CultureInfo.InvariantCulture);
                    if (missing0[v0] == c0) { t += ",(USER MISSING)"; }
                    t += "\r\n"; sb.Append(t); t = "";
                }
            }
            for (UInt16 v = 0; (v < Vc); v++)
            {
                t = "Dependent Variable : " + VarLabels[v].Trim();
                t += "\r\n"; sb.Append(t); t = "";
                t = "New code";
                t += ",";
                t += "Old code";
                t += ",";
                t += "Category N";
                t += "\r\n"; sb.Append(t); t = "";
                for (UInt16 c = 0; (c < NcVc[v]); c++)
                {
                    t = c.ToString("0", CultureInfo.InvariantCulture);
                    t += ",";
                    if (code[v, c] != null) { t += code[v, c].Trim(); } else { t += "null"; };
                    t += ",";
                    t += freq[v, c].ToString("0", CultureInfo.InvariantCulture);
                    if (missing0[v] == c) { t += ",(USER MISSING)"; }
                    t += "\r\n"; sb.Append(t); t = "";

                }
            }
            for (UInt16 v1 = 0; (v1 < Vc1); v1++)
            {
                t = "Singular Variable : " + Var1Labels[v1].Trim();
                t += "\r\n"; sb.Append(t); t = "";
                t = "New code";
                t += ",";
                t += "Old code";
                t += ",";
                t += "Category N";
                t += "\r\n"; sb.Append(t); t = "";
                for (UInt16 c1 = 0; (c1 < NcVc1[v1]); c1++)
                {
                    t = c1.ToString("0", CultureInfo.InvariantCulture);
                    t += ",";
                    if (code1[v1, c1] != null) { t += code1[v1, c1].Trim(); } else { t += "null"; };
                    t += ",";
                    t += freq1[v1, c1].ToString("0", CultureInfo.InvariantCulture);
                    if (missing0[v1] == c1) { t += ",(USER MISSING)"; }
                    t += "\r\n"; sb.Append(t); t = "";
                }
            }
            t += "\r\n"; t += "\r\n"; sb.Append(t); t = "";
            inform("End Recoding Identifiers");
            return sb;
        }


        //Save likelihoods for further us (not implemented in thsi app version)
        public void LikelihoodsSave(string LikelihoodFile)
        {

            string FileFirst = "";




            try
            {
                BinaryWriter bw = new BinaryWriter(new FileStream(LikelihoodFile,
                  FileMode.Create));
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message + "\n Cannot create file.");
                return;
            }
            try
            {

                BinaryWriter bw = new BinaryWriter(new FileStream(LikelihoodFile,
                  FileMode.Create));

                bw.Write(Vc0);
                bw.Write(Vc);
                bw.Write(Vc1);

                bw.Write(CriteriumHandling);
                bw.Write(ThresHoldPercentageCriterium);
                bw.Write(ThresHoldCriterium);
                bw.Write(DifferenceCriterium);
                bw.Write(AutoSelection);
                bw.Write(Linkmethod);
                bw.Write(MissingsInclude);


                for (UInt16 v0 = 0; v0 < Vc0; v0++)
                {
                    for (UInt16 v = 0; (v < Vc); v++)
                    {
                        bw.Write(SelectVar[v0, v]);
                    }
                }
                for (UInt16 v1 = 0; v1 < Vc1; v1++)
                {
                    bw.Write(SelectVar1[v1]);
                }
                for (UInt16 v0 = 0; v0 < Vc0; v0++)
                {
                    bw.Write(missing0[v0]);
                }
                for (UInt16 v = 0; v < Vc; v++)
                {
                    bw.Write(missing[v]);
                }
                for (UInt16 v1 = 0; v1 < Vc1; v1++)
                {
                    bw.Write(missing1[v1]);
                }
                for (UInt16 v0 = 0; v0 < Vc0; v0++)
                {
                    bw.Write(NcVc0[v0]);
                }
                for (UInt16 v = 0; v < Vc; v++)
                {
                    bw.Write(NcVc[v]);
                }
                for (UInt16 v1 = 0; v1 < Vc1; v1++)
                {
                    bw.Write(NcVc1[v1]);
                }
                for (UInt16 v0 = 0; v0 < Vc0; v0++)
                {
                    for (UInt16 v = 0; v < Vc; v++)
                    {
                        if ((SelectVar[v0, v] == 1))
                        {
                            for (UInt16 c0 = 0; (c0 < NcVc0[v0]); c0++)
                            {
                                for (UInt16 c = 0; (c < NcVc[v]); c++)
                                {

                                    UInt64 i = posLi[v0, v] + (ulong)c0 * NcVc[v] + c;
                                    bw.Write(sPPratio[i]);

                                }
                            }
                        }
                    }
                }
                for (UInt16 v1 = 0; (v1 < Vc1); v1++)
                {
                    for (UInt16 c = 0; (c < NcVc1[v1]); c++)
                    {
                        bw.Write(ePPratio[c, v1]);
                    }
                }

            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message + "\n Cannot write to file.");
                return;

            }

            Console.ReadKey();

            //SAVE ALSO IN TEXT..            
            ResultFile = (FileFirst + ".dsc");

            //Saving Likelihoods in text;
            for (UInt16 v0 = 0; (v0 < Vc0); v0++)
            {
                for (UInt16 v = 0; (v < Vc); v++)
                {
                    if ((SelectVar[v0, v] == 1))
                    {
                        for (UInt16 c0 = 0; (c0 < NcVc0[v0]); c0++)
                        {
                            for (UInt16 c = 0; (c < NcVc[v]); c++)
                            {
                                t = "";
                                t = (t + Var0Labels[v0]);
                                t = (t + Delimeter);
                                t = (t + VarLabels[v]);
                                t = (t + Delimeter);
                                t = (t + code0[v0, c0]);
                                t = (t + Delimeter);
                                t = (t + code[v, c]);
                                t = (t + Delimeter);


                                UInt64 i = posLi[v0, v] + (ulong)c0 * NcVc[v] + c;
                                t = (t + sPPratio[i].ToString("0", CultureInfo.InvariantCulture));

                                //save to text file                                     
                            }
                        }
                    }
                }
            }
            for (UInt16 v1 = 0; (v1 < Vc1); v1++)
            {
                for (UInt16 c = 0; (c < NcVc1[v1]); c++)
                {
                    t = "";
                    t += Var1Labels[v1];
                    t += Delimeter;
                    t += code1[v1, c];
                    t += Delimeter;
                    t += ePPratio[c, v1].ToString("0", CultureInfo.InvariantCulture);
                    //save to text file  
                }
            }
        }

        //Get likelihoods to use again (not implemented in thsi app version)
        public StringBuilder LikelihoodsGet(string LikelihoodFile, string FileName)
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                BinaryReader br = new BinaryReader(new FileStream(LikelihoodFile,
                   FileMode.Open));
            }
            catch (IOException e)
            {
                sb.AppendLine("\n Cannot open file.");
                return sb;
            }
            try
            {

                BinaryReader br = new BinaryReader(new FileStream(LikelihoodFile,
                 FileMode.Open));
                //      i = br.ReadInt32();
                //      Console.WriteLine("Integer data: {0}", i);
                //      d = br.ReadDouble();
                //      Console.WriteLine("Double data: {0}", d);
                //      b = br.ReadBoolean();
                //      Console.WriteLine("Boolean data: {0}", b);
                //      s = br.ReadString();
                //      Console.WriteLine("String data: {0}", s);

                inform("Getting Likelihoods");

                UInt16 nVc0;
                UInt16 nVc;
                UInt16 nVc1;

                nVc0 = (UInt16)br.Read();  // + 32767);
                nVc = (UInt16)br.Read();   // + 32767);
                nVc1 = (UInt16)br.Read();  // + 32767);

                if ((nVc0 != Vc0))
                {
                    br.Close();
                    sb.AppendLine("Number of Independent Variables in the Likelihood File is Inconsistent with then number of Independent Variables in the System File.");
                    return sb;
                }

                if ((nVc != Vc))
                {
                    br.Close();
                    sb.AppendLine("Number of dependent Variables in the Likelihood File is Inconsistent with then number of dependent Variables in the System File.");
                    return sb;
                }

                if ((nVc1 != Vc1))
                {
                    br.Close();
                    sb.AppendLine("Number of Singular Variables in the Likelihood File is Inconsistent with then number of Singular Variables in the System File.");
                    return sb;
                }

                CriteriumHandling = br.ReadInt32();
                ThresHoldPercentageCriterium = br.ReadInt32();
                ThresHoldCriterium = br.ReadInt32();
                DifferenceCriterium = br.ReadInt32();
                AutoSelection = br.ReadInt32();
                Linkmethod = br.ReadInt32();
                MissingsInclude = br.ReadInt32();

                //        redim SelectVar;
                //        redim SelectVar1;
                //        redim NcVc0;
                //        redim NcVc;


                for (UInt16 v0 = 0; (v0 < Vc0); v0++)
                {
                    for (UInt16 v = 0; (v < Vc); v++)
                    {
                        SelectVar[v0, v] = br.ReadByte();
                    }
                }
                for (UInt16 v1 = 0; (v1 < Vc1); v1++)
                {
                    SelectVar1[v1] = br.ReadByte();
                }
                for (UInt16 v0 = 0; (v0 < Vc0); v0++)
                {
                    missing0[v0] = br.ReadUInt16();
                }
                for (UInt16 v = 0; (v < Vc); v++)
                {
                    missing[v] = br.ReadUInt16();
                }
                for (UInt16 v1 = 0; (v1 < Vc1); v1++)
                {
                    missing1[v1] = br.ReadUInt16();
                }
                NcVc0H = 0;
                for (UInt16 v0 = 0; (v0 < Vc0); v0++)
                {
                    NcVc0[v0] = br.ReadUInt16();
                    if ((NcVc0[v0] > NcVc0H))
                    {
                        NcVc0H = NcVc0[v0];
                    }
                }
                NcVcH = 0;
                for (UInt16 v = 0; (v < Vc); v++)
                {
                    NcVc[v] = br.ReadUInt16();
                    if ((NcVc[v] > NcVcH))
                    {
                        NcVcH = NcVc[v];
                    }
                }
                NcVc1H = 0;
                for (UInt16 v1 = 0; (v1 < Vc1); v1++)
                {
                    NcVc1[v1] = br.ReadUInt16();
                    if ((NcVc1[v1] > NcVc1H))
                    {
                        NcVc1H = NcVc1[v1];
                    }
                }
                //        redim PPratio;
                //        redim ePPratio;
                for (UInt16 v0 = 0; (v0 < Vc0); v0++)
                {
                    for (UInt16 v = 0; (v < Vc); v++)
                    {
                        if ((SelectVar[v0, v] == 1))
                        {
                            for (UInt16 c0 = 0; (c0 < NcVc0[v0]); c0++)
                            {
                                for (UInt16 c = 0; (c < NcVc[v]); c++)
                                {

                                    UInt64 i = posLi[v0, v] + (ulong)c0 * NcVc[v] + c; ;
                                    sPPratio[i] = br.ReadInt32();
                                }
                            }
                        }
                    }
                }
                for (UInt16 v1 = 0; (v1 < Vc1); v1++)
                {
                    for (UInt16 c = 0; (c < NcVc1[v1]); c++)
                    {
                        ePPratio[c, v1] = br.ReadInt32();
                    }
                }
                br.Close();
            }
            catch (IOException e)
            {

                sb.AppendLine("\n Cannot read from file.");
                return sb;
            }


            IterNumber = 0;
            t = "{\\deff \\pard ";
            t = (t + "\\par \\par ");
            t = (t + ("{\\b \\fs24 SUB REPORT : " + SubReportID));
            String Day = DateTime.Now.Day.ToString();
            String Month = DateTime.Now.Month.ToString();
            String Year = DateTime.Now.Year.ToString();
            String Hour = DateTime.Now.Hour.ToString();
            String Minute = DateTime.Now.Minute.ToString();
            t += Month + Day + Year + Minute + Year;
            t = (t + "\\par \\par ");
            t = (t + ("{\\b \\fs20 Likelihoods from File : "
                        + (FileName + " }")));
            t = (t + "\\par \\par ");
            t = (t + "\\par \\par ");
            t = (t + "}");
            //                     ;
            //                     outfrm.out.SelStart = outfrm.out.text.Length;
            //LikelihoodSource = 1;
            //LinkRecords(ThresHoldPercentageCriterium, LikelihoodSource, 0, Linkmethod,"");
            inform("");

            sb.AppendLine("ok");
            return sb;
        }
         


        public void inform(string message) { }


        public void AutoSelectVariables()
        {
            double hc = 0;
            UInt16 hh;
            for (UInt16 v0 = 0; (v0 < Vc0); v0++)
            {
                for (UInt16 v = 0; (v < Vc); v++)
                {
                    SelectVar[v0, v] = 0;
                }
            }
            for (UInt16 v0 = 0; (v0 < Vc0); v0++)
            {
                // TODO: # ... Warning!!! not translated
                hh = 0;
                for (UInt16 v = 0; (v < Vc); v++)
                {
                    // If Chip[v0, v] < 0.0001 Then
                    //  If Chip[v0, v] < hc Then hc = Chip[v0, v]: hh = v
                    // End If
                    if ((CHIx[v0, v] > 999))
                    {
                        if ((CHIx[v0, v] < hc))
                        {
                            hc = CHIx[v0, v];
                        }
                        hh = v;
                    }
                }
                SelectVar[v0, hh] = 1;
            }
            //  For v0 = 1 To Vc0
            //   For v = 1 To Vc
            //    If Chip[v0, v] < 0.0001 Then SelectVar[v0, v] = 1 Else SelectVar[v0, v] = 0
            //   Next v
            //  Next v0
        }

        public StringBuilder LinkRecords(int CriteriumHandling, double ThresHoldCriterium, double ThresHoldPercentageCriterium, byte IterNumber, int Linkmethod, System.IO.StreamWriter fileScores, Boolean writeLinkage)
        {

            StringBuilder sb = new StringBuilder();

            int Nident;
            Int64[] Nrecords = new Int64[maxRperRecordS];

            double Threshold = 0;
            double Difference;


            if ((IterNumber == 0))
            {

                t = "Determine Start Classification";

                //LikelihoodSource = 0;
            }
            t += "\r\n";
            sb.Append(t); t = "";
            //  COMPUTE CHI or RATIO per RECORD IN SET

            switch (Linkmethod)
            {
                case -1:
                    t = "Linkage Options : Bayesian Linkage (obs/exp) ";
                    break;
                case 0:
                    t = "Linkage Options : Bayesian Linkage";
                    break;
                case 1:
                    t = "Linkage Options : Chi Square Linkage";
                    break;
            }
            t += "\r\n";
            sb.Append(t); t = "";
            switch (CriteriumHandling)
            {
                case 1:
                    t = "Linkage Method : Best Fit in BLock ";
                    break;
                case 2:
                    t = "Linkage Method : Best Fit in BLock Above Threshold";
                    break;
                case 3:
                    t = "Linkage Method : Fits in BLock Above Threshold";
                    break;
                case 4:
                    t = "Linkage Method :Fits in BLock Above Threshold Given Difference";
                    break;
            }
            t += "\r\n";
            sb.Append(t); t = "";
            if ((ThresHoldCriterium == -99))
            {
                t = "Threshold Criterium: ,Automatic : "
                             + ThresHoldCriterium.ToString("0.00", CultureInfo.InvariantCulture) + " based on percentage : "
                             + ThresHoldPercentageCriterium.ToString("0.00", CultureInfo.InvariantCulture) + " resulting in threshold : "
                             + Threshold.ToString("0.00", CultureInfo.InvariantCulture) + " }";
            }
            else
            {
                t = "Threshold Criterium : ,User Specified : "
                            + ThresHoldCriterium.ToString("0.00", CultureInfo.InvariantCulture) + " }";
            }
            t += "\r\n"; sb.Append(t); t = "";
            if ((DifferenceCriterium == -99))
            {
                t = "Difference Criterium : Automatic : " + DifferenceCriterium.ToString("0.00", CultureInfo.InvariantCulture);
            }
            else
            {
                t = "Difference Criterium : User Specified : " + DifferenceCriterium.ToString("0.00", CultureInfo.InvariantCulture);
            }
            t += "\r\n"; sb.Append(t); t = "";
            if ((AutoSelection == 1))
            {
                t = "Selection of Identifiers : Automatic";
            }
            else
            {
                t = "Selection of Identifiers : User Specified";
            }
            t += "\r\n"; sb.Append(t); t = "";
            t += "\r\n"; sb.Append(t); t = "";

            if (SelectedIdentifiersShow == 1)
            {
                t = "Selected Independent Identifiers : "; t += ",";
                Nident = 0;
                for (UInt16 v0 = 0; (v0 < Vc0); v0++)
                {
                    for (UInt16 v = 0; (v < Vc); v++)
                    {
                        if ((SelectVar[v0, v] == 1))
                        {
                            Nident = (Nident + 1);
                            t += "Indep - Dep " + Nident.ToString("0", CultureInfo.InvariantCulture) + " :";
                            t += ",";
                            t += Var0Labels[v0] + " - " + VarLabels[v];

                        }
                    }
                    t += "\r\n"; sb.Append(t); t = "";
                }

                t = "Selected Dependent Identifiers : "; t += ",";
                for (UInt16 v1 = 0; (v1 < Vc1); v1++)
                {
                    if ((SelectVar1[v1] == 1))
                    {
                        Nident = (Nident + 1);
                        t += "Singular " + Nident.ToString("0", CultureInfo.InvariantCulture) + " :";
                        t += ",";
                        t += Var1Labels[v1];
                    }
                }
                t += "\r\n"; sb.Append(t); t = "";
                t += "\r\n"; sb.Append(t); t = "";
            }




            if (Linkmethod == 0)
            {

                UInt64 PrNow = 0;
                for (UInt64 s = 1; (s <= Ns); s++)
                {

                    //if (Math.Round(Convert.ToSingle(s) / 1000) == Convert.ToSingle(s) / 1000) { saveStatus(statusFileName, "linkmethod = Ratio, linking step: " + IterNumber.ToString() + ", record: " + s.ToString()); }
                    UInt16[,] SRdata = new UInt16[maxRperRecordS, maxVnow];


                    for (UInt16 v0 = 0; v0 < Vc0; v0++)
                    {
                        SRdata[0, v0] = SetVar0[s, v0];

                    }
                    for (byte r = 1; r <= Nrr[s]; r++)
                    {
                        for (UInt16 v = 0; v < Vc; v++)
                        {
                            SRdata[r, v] = SetVar[PrNow + r, v];
                        }
                        for (UInt16 v1 = 0; v1 < Vc1; v1++)
                        {
                            SRdata[r, Vc + v1] = SetVar1[PrNow + r, v1];
                        }
                    }


                    if ((IterNumber > 0))
                    {
                        for (UInt16 v1 = 0; v1 < Vc1; v1++)
                        {
                            if (SelectVar1[v1] == 1)
                            {
                                inform("Computing Ratio\'s : Indepent "
                                 + v1.ToString("0", CultureInfo.InvariantCulture)); // strange:??? + " with Dependent " + v0.ToString("0", CultureInfo.InvariantCulture));

                                for (byte r = 1; r <= Nrr[s]; r++)
                                {
                                    if (SRdata[r, v1] != missing1[v1] || MissingsInclude == 1)
                                    {
                                        score[(PrNow + r)] = score[PrNow + r] * ePPratio[SRdata[r, Vc + v1], v1];

                                    }
                                }
                            }
                        }
                    }


                    for (byte r = 1; r <= Nrr[s]; r++)
                    {
                        if ((score[(PrNow + r)] > 0))
                        {
                            score[(PrNow + r)] = Convert.ToSingle(Math.Log(Convert.ToDouble(score[(PrNow + r)])));
                        }
                        else
                        {
                            score[(PrNow + r)] = -7;
                        }
                        if ((score[(PrNow + r)] < -7))
                        {
                            score[(PrNow + r)] = -7;
                        }

                        if (writeLinkage)
                        {
                            fileScores.WriteLine(Addscore(IterNumber, s, r, PrNow + r, score[PrNow + r], ThresHoldCriterium)); 

                        }
                    }
                    PrNow += Nrr[s];
                }
            }
            if (Linkmethod == 1)
            {
                UInt64 PrNow = 0;
                for (UInt64 s = 1; (s <= Ns); s++)
                {
                    //if (Math.Round(Convert.ToSingle(s) / 1000) == Convert.ToSingle(s) / 1000) { saveStatus(statusFileName, "linkmethod = Chi Square, linking step: " + IterNumber.ToString() + ", record: " + s.ToString()); }
                    UInt16[,] SRdata = new UInt16[maxRperRecordS, maxVnow];


                    for (UInt16 v0 = 0; v0 < Vc0; v0++)
                    {
                        SRdata[0, v0] = SetVar0[s, v0];

                    }
                    for (byte r = 1; r <= Nrr[s]; r++)
                    {
                        for (UInt16 v = 0; v < Vc; v++)
                        {
                            SRdata[r, v] = SetVar[PrNow + r, v];
                        }
                        for (UInt16 v1 = 0; v1 < Vc1; v1++)
                        {
                            SRdata[r, Vc + v1] = SetVar1[PrNow + r, v1];
                        }
                    }

                    for (byte r = 1; r <= Nrr[s]; r++)
                    {
                        score[(PrNow + r)] = 0;
                    }
                    for (byte r = 1; r <= Nrr[s]; r++)
                    {
                        for (UInt16 v0 = 0; (v0 < Vc0); v0++)
                        {
                            for (UInt16 v = 0; (v < Vc); v++)
                            {
                                if ((SelectVar[v0, v] == 1))
                                {
                                    //inform("Computing Likelihoods : Indepent " + v0.ToString("0", CultureInfo.InvariantCulture) + " with Dependent " + v.ToString("0", CultureInfo.InvariantCulture));



                                    UInt64 i = posLi[v0, v] + SRdata[0, v0] * (ulong)NcVc[v] + SRdata[r, v];
                                    score[PrNow + r] += sPPChi[i];



                                }
                            }
                        }
                        score[PrNow + r] /= (Ns / 10);  //Convert.ToSingle(Math.Pow(Ns,1));

                        if (writeLinkage)
                        {
                            fileScores.WriteLine(Addscore(IterNumber, s, r, PrNow + r, score[PrNow + r], ThresHoldCriterium));  //);
                        }

                    }
                    PrNow += Nrr[s];
                }
            }
            //fileScores.Close();


            //double[] scores = new double[MaxGrCat];
            Nsmall = 0;
            byte rh;
            double ll = 99999;
            double hh = -99999;
            double lh;

            UInt64 PrNow2 = 0; Byte NrNow = 0; //Int64 PrNowTest = 0;
            for (UInt64 s = 1; s <= Ns; s++)
            {
                NrNow = Nrr[s];

                for (byte r = 1; r <= NrNow; r++)
                {
                    // 'If score(Pr[s] + r) <> -100 Then
                    if (score[PrNow2 + r] > hh)
                    {
                        hh = score[PrNow2 + r];
                    }
                    if (score[PrNow2 + r] < ll)
                    {
                        ll = score[PrNow2 + r];
                    }
                    // 'Else
                    // ' Nsmall = Nsmall + 1
                    // 'End If
                }
                PrNow2 += NrNow;
            }

            inform("");
            // CATAGORY RANGE 1- 100
            int cNow = 0;
            lh = (hh - ll);


            PrNow2 = 0;

            if (lh != 0)
            {
                for (UInt16 c = 0; (c <= NcatGraph); c++)
                {
                    scoresV[c] = ll + c * (hh - ll) / NcatGraph;
                    scoresN[c] = 0;
                }
                for (UInt64 s = 1; (s <= Ns); s++)
                {

                    NrNow = Nrr[s];

                    for (byte r = 1; r <= NrNow; r++)
                    {
                        scoresN[Convert.ToInt32(-.5 + NcatGraph * (score[PrNow2 + r] - ll) / (hh - ll))] += 1;
                    }
                    PrNow2 += NrNow;
                }
            }
            else
            {
                for (byte c = 0; (c <= NcatGraph); c++)
                {
                    scoresV[c] = 0;
                    scoresN[c] = 0;
                }
            }

            // THRESHOLDCRITERIUM
            if ((ThresHoldCriterium == -99))
            {
                Threshold = ThresHoldScore(ThresHoldPercentageCriterium);
                //   For h = NcatGraph To 0 Step -1
                //    tel = tel + Fix(scores(h))
                //    If tel <= Ns * 0.95 Then Threshold = (h * lh) + ll
                //   Next h
            }
            else
            {
                Threshold = ThresHoldCriterium;
            }
            // DIFFERENCECRITERIUM
            if ((DifferenceCriterium == -99))
            {
                Difference = 0.0001;
            }
            else
            {
                Difference = DifferenceCriterium;
            }
            // CRITERIUMHANDLING
            blocksLinkedN = 0;
            double h = 0;
            switch (CriteriumHandling)
            {



                case 1:
                    PrNow2 = 0;
                    for (UInt64 s = 1; (s <= Ns); s++)
                    {

                        NrNow = Nrr[s];

                        h = -99999;
                        rh = 0;
                        for (byte r = 1; r <= NrNow; r++)
                        {
                            if (score[PrNow2 + r] > h)
                            {
                                h = score[PrNow2 + r];
                                rh = r;
                            }

                        }
                        Hscore[PrNow2 + rh] = 1;
                        blocksLinkedN = (blocksLinkedN + 1);
                        PrNow2 += NrNow;
                    }
                    break;
                case 2:
                    PrNow2 = 0;
                    for (UInt64 s = 1; (s <= Ns); s++)
                    {
                        NrNow = Nrr[s];

                        h = -99999; Boolean linkedNow = false;
                        rh = 0;
                        for (byte r = 1; r <= NrNow; r++)
                        {
                            if ((score[PrNow2 + r] > h))
                            {
                                h = score[PrNow2 + r];
                                rh = r;
                            }

                        }
                        if (score[PrNow2 + rh] > Threshold)
                        {
                            Hscore[PrNow2 + rh] = 1;
                            linkedNow = true;
                        }
                        if (linkedNow) { blocksLinkedN = (blocksLinkedN + 1); }
                        PrNow2 += NrNow;

                    }
                    break;
                case 3:
                    PrNow2 = 0;
                    for (UInt64 s = 1; (s <= Ns); s++)
                    {
                        NrNow = Nrr[s];

                        h = -99999; Boolean linkedNow = false;
                        for (byte r = 1; r <= NrNow; r++)
                        {

                            //zie 2!!
                            if (score[PrNow2 + r] > h && score[PrNow2 + r] > Threshold)
                            {
                                //h = score[PrNow2 + r];
                                Hscore[PrNow2 + r] = 1;
                                linkedNow = true;
                            }

                        }
                        if (linkedNow) { blocksLinkedN = (blocksLinkedN + 1); }
                        PrNow2 += NrNow;
                        //PrNowTest += Nrr[s];
                    }
                    break;
                case 4:
                    PrNow2 = 0;
                    for (UInt64 s = 1; (s <= Ns); s++)
                    {
                        NrNow = Nrr[s];

                        h = -99999;
                        rh = 0;
                        for (byte r = 1; r <= NrNow; r++)
                        {
                            if (score[PrNow2 + r] > h && score[PrNow2 + r] > Threshold)
                            {
                                h = score[PrNow2 + r];
                                rh = r;
                                Hscore[PrNow2 + r] = 1;
                                blocksLinkedN = (blocksLinkedN + 1);
                            }

                        }
                        for (byte r = 1; r <= Nrr[s]; r++)
                        {
                            if (score[PrNow2 + rh] - score[PrNow2 + r] > DifferenceCriterium)
                            {
                                Hscore[PrNow2 + r] = 0;
                            }
                        }
                        PrNow2 += NrNow;
                    }
                    break;
            }
            // DISTRIBUTION OF NUMBER OF LINKED RECORDS PER SET
            PrNow2 = 0; int Nin = 0;
            for (UInt64 s = 1; (s <= Ns); s++)
            {
                NrNow = Nrr[s];

                Nin = 0;
                for (byte r = 1; r <= NrNow; r++)
                {
                    //Nin = 0;
                    if (Hscore[PrNow2 + r] == 1)
                    {
                        Nin += 1;
                    }

                }
                Nrecords[Nin] += 1;
                PrNow2 += NrNow;
            }
            if ((DistributionLikelihoodsShow == 1))
            {

                switch (Linkmethod)
                {
                    case -1:
                        t = "Distribution of Likelihoods (obs/exp) : Link step : "
                                    + (IterNumber + 1).ToString("0", CultureInfo.InvariantCulture) + " (N = "
                                    + Nsmall.ToString("0", CultureInfo.InvariantCulture) + " < -99)";
                        break;
                    case 0:
                        t = "Distribution of Likelihoods : Link step : "
                                    + (IterNumber + 1).ToString("0", CultureInfo.InvariantCulture) + " (N = "
                                    + Nsmall.ToString("0.00", CultureInfo.InvariantCulture) + " < -99)";
                        break;
                    case 1:
                        t = "Distribution of CHI Squares : link step : " + (IterNumber + 1).ToString("0", CultureInfo.InvariantCulture);
                        break;
                }
                t += "\r\n"; t += "\r\n"; sb.Append(t);
                double HighNow = 0;
                for (byte c = 0; (c <= NcatGraph); c++)
                {
                    if (scoresN[c] > HighNow) { HighNow = scoresN[c]; }
                }
                for (byte c = 0; (c <= NcatGraph); c++)
                {
                    t = scoresV[c].ToString("0.00", CultureInfo.InvariantCulture);
                    t += ",";
                    if (HighNow > 0) { for (int n = 1; n <= (ushort)100 * (scoresN[c] / HighNow); n++) { t += "*"; } }
                    t += "\r\n"; sb.Append(t); t = "";
                }

            }
            if ((LinkedSetsShow == 1))
            {


                switch (Linkmethod)
                {
                    case -1:
                        t = "Likelihoods (obs/exp) : Link step : "
                           + (IterNumber + 1).ToString("0", CultureInfo.InvariantCulture) + " (N = "
                           + Nsmall.ToString("0", CultureInfo.InvariantCulture) + " < -99)";
                        break;
                    case 0:
                        t = "Likelihood Ratios per record in recordset : Link step : "
                          + (IterNumber + 1).ToString("0", CultureInfo.InvariantCulture) + " (N = "
                          + Nsmall.ToString("0", CultureInfo.InvariantCulture) + " < -99)";
                        break;
                    case 1:
                        t = "Chi Squares per record in recordset  : Link step : "
                        + (IterNumber + 1).ToString("0", CultureInfo.InvariantCulture) + " (N = "
                        + Nsmall.ToString("0", CultureInfo.InvariantCulture) + " < -99)";
                        break;
                }

                for (byte r = 1; (r <= 9); r++)
                {
                    t += ",";
                    t += "record " + r.ToString("0", CultureInfo.InvariantCulture);
                }
                t += "\r\n"; sb.Append(t); t = "";
                PrNow2 = 0;
                for (UInt64 s = 1; (s <= Ns); s++)
                {
                    NrNow = Nrr[s];

                    // Exit For
                    t = (t + ",");
                    t = (t + ("set " + s.ToString("0", CultureInfo.InvariantCulture)));
                    for (byte r = 1; r <= NrNow; r++)
                    {
                        t = (t + ",");
                        t += score[PrNow2 + r].ToString("0.00", CultureInfo.InvariantCulture);
                        if (Hscore[PrNow2 + r] == 1)
                        {
                            t = (t + "*");
                        }
                    }
                    t += "\r\n"; sb.Append(t); t = "";
                    if (Convert.ToUInt64(s / 20) == s / 20)
                    {
                        inform(("Linking set number " + s.ToString("0", CultureInfo.InvariantCulture)));
                    }
                    if ((s > 59))
                    {
                        break;
                    }
                    PrNow2 += NrNow;
                }
                t += "\r\n"; sb.Append(t); t = "";

            }
            // ' t = "{"
            // '   t = t & "\deff "
            // '   For c = 1 To 10
            // '    t = t & "\tx" & Format(2000 + (c - 1) * 1000)
            // '   Next c
            // '   If i = 0 Then
            // '    t = t & "{\b Likelihood Ratio's : Independent : " & Var0Labels(v0) & " and dependent : " & VarLabels(v) & " : p = " & Format(Chip[v0, v], " 0.0000") & "}"
            // '    t = t & "\par \par "



            if ((DistributionLinkedRecordsNShow == 1))
            {
                t = "Link step : " + (IterNumber + 1).ToString("0", CultureInfo.InvariantCulture) + " : ";

                switch (Linkmethod)
                {
                    case -1:
                        t += "Linked records per link set : (Criterium Likelihood Ratio (only obs/exp) > "
                                    + Threshold.ToString("0.00", CultureInfo.InvariantCulture) + ") ";
                        break;
                    case 0:
                        t += "Linked records per link set : (Criterium Likelihood Ratio > "
                                    + Threshold.ToString("0.00", CultureInfo.InvariantCulture) + ") ";
                        break;
                    case 1:
                        t += "Linked records per link set : (Criterium CHI Square > "
                                    + Threshold.ToString("0.00", CultureInfo.InvariantCulture) + ") ";
                        break;
                }

                t += "\r\n"; t += "\r\n"; sb.Append(t); t = "";

                t = "Blocks with a link: " + blocksLinkedN.ToString("0", CultureInfo.InvariantCulture) + " : " + (Convert.ToSingle(blocksLinkedN) / Convert.ToSingle(Ns)).ToString("0.00%", CultureInfo.InvariantCulture);

                t += "\r\n"; t += "\r\n"; sb.Append(t); t = "";

                for (byte r = 0; (r <= 9); r++)
                {

                    t += "N = " + r.ToString("0", CultureInfo.InvariantCulture);
                    t += " : ";
                    if (Ns > 0) t += (Convert.ToSingle(Nrecords[r]) / Convert.ToSingle(Ns)).ToString("0.00%", CultureInfo.InvariantCulture);
                    t += "\r\n";
                }
                t += "\r\n"; t += "\r\n"; sb.Append(t); t = "";

            }



            inform("");
            return sb;
        }


        public string Addscore(byte iterN, UInt64 s, byte r, UInt64 rt, double score, double ThresHoldCriterium)
        {

            //char dChar = Delimeter[0];

            t = ""; //file.WriteLine(t);


            //t += iterN.ToString("0", CultureInfo.InvariantCulture);
            // += Delimeter;
            t += s.ToString("0", CultureInfo.InvariantCulture);
            t += Delimeter;
            t += r.ToString("0", CultureInfo.InvariantCulture);
            t += Delimeter;
            t += RespNr0[s];
            t += Delimeter;
            //t += RespNr[Pr[s] + r];
            t += RespNr[rt];
            t += Delimeter;
            t += score.ToString("0.00", CultureInfo.InvariantCulture);
            //t += Delimeter;
            //t += ThresHoldCriterium.ToString("0.00", CultureInfo.InvariantCulture);
            //t += Delimeter;
            //t = t + Hscore[Pr[s] + r].ToString("0.00", CultureInfo.InvariantCulture);


            return t;
        }

        public double ThresHoldScore(double freq)
        {
            double ThresHoldScore2 = 0;

            //REDO WITH GRAPH ALGORITMEN

            return ThresHoldScore2;
        }


        public UInt64 KeyCachSaveValueK(FileStream cach, BinaryWriter writer, FileStream cachI, BinaryWriter writerI, UInt64 s, Byte nr, UInt64 posOld, string[] respnrI)
        {
            UInt64 posNew = posOld + 1; //1 for nr

            for (byte r = 0; r < nr + 1; r++)
            {
                posNew = Convert.ToUInt64(respnrI[r].Length + 2);

            }
            cach.Position = Convert.ToInt64(8 * (s - 1));
            writer.Write(posOld);

            cachI.Position = Convert.ToInt64(posOld); //int.MaxValue + posOld;
            writerI.Write(nr);
            for (byte r = 0; r < nr + 1; r++)
            {
                writerI.Write(respnrI[r]);
            }
            return posNew;
        }

        public string[] KeyCachGetValueK(FileStream cach, BinaryReader reader, FileStream cachI, BinaryReader readerI, UInt64 s)
        {
            string[] respnrI = new string[maxRperRecordS];
            cach.Position = Convert.ToInt64(8 * (s - 1));
            Int64 pos = reader.ReadInt64();

            cachI.Position = pos;
            Byte nr = readerI.ReadByte();
            for (byte r = 0; r < nr + 1; r++)
            {
                respnrI[r] = readerI.ReadString();
            }
            return respnrI;
        }

        public UInt64 varCachSaveValueK(FileStream cach, BinaryWriter writer, FileStream cachI, BinaryWriter writerI, UInt64 s, Byte nr, UInt64 posOld, UInt16[,] SRdata)
        {

            UInt64 posNew = posOld + 1 + (ulong)2 * Vc0 + (ulong)2 * nr * Vc + (ulong)4 * nr * Vc1;
            cach.Position = Convert.ToInt64(8 * (s - 1));
            writer.Write(posOld);
            //        varCachSaveValueI(vcachI, vbwI, s, nr, posOld, SRdata);
            //        return posNew;
            //    }
            //    public void varCachSaveValueI(FileStream cach, BinaryWriter writer, Int64 s, Byte nr, Int64 posOld, Int16[,] SRdata)
            //    {
            cachI.Position = Convert.ToInt64(posOld); //int.MaxValue + posOld;
            writerI.Write(nr);

            for (UInt16 v0 = 0; v0 < Vc0; v0++)
            {
                writerI.Write(SRdata[0, v0]);
            }
            for (byte r = 1; r < nr + 1; r++)
            {
                for (UInt16 v = 0; v < Vc; v++)
                {
                    writerI.Write(SRdata[r, v]);
                }
                for (UInt16 v1 = 0; v1 < Vc1; v1++)
                {
                    writerI.Write(SRdata[r, Vc + v1]);
                }
            }
            return posNew;
        }

        public UInt16[,] varCachGetValueK(FileStream cach, BinaryReader reader, FileStream cachI, BinaryReader readerI, UInt64 s)
        {
            UInt16[,] SRdata = new UInt16[maxRperRecordS, maxVnow];
            cach.Position = Convert.ToInt64(8 * (s - 1));
            Int64 pos = reader.ReadInt64();   //1 te ver???
                                              //       Int16[,] SRdata2= varCachGetValueI(vcachI, vbrI, pos);     
                                              //       return SRdata2;
                                              //   }
                                              //   public Int16[,] varCachGetValueI(FileStream cach, BinaryReader reader, Int64 pos)
                                              //   {
                                              //       Int16[,] SRdata = new Int16[maxRperRecordS, maxVar];
            cachI.Position = pos;
            Nrr[s] = readerI.ReadByte();
            ///SRdata[0, 0] = nr;
            for (UInt16 v0 = 0; v0 < Vc0; v0++)
            {
                SRdata[0, v0] = readerI.ReadUInt16();
            }
            for (byte r = 1; r < Nrr[s] + 1; r++)
            {
                for (UInt16 v = 0; v < Vc; v++)
                {
                    SRdata[r, v] = readerI.ReadUInt16();
                }
                for (UInt16 v1 = 0; v1 < Vc1; v1++)
                {
                    SRdata[r, Vc + v1] = readerI.ReadUInt16();
                }
            }

            return SRdata;
        }


        public Byte varCachGetValueRK(FileStream cach, BinaryReader reader, FileStream cachI, BinaryReader readerI, UInt64 s)
        {
            //Int16[,] SRdata = new Int16[maxRperRecordS, maxVar];

            cach.Position = Convert.ToInt64(8 * (s - 1));
            Int64 pos = reader.ReadInt64();
            cachI.Position = pos;
            Byte r = readerI.ReadByte();
            return r;
        }

        public void ProbCachSaveValue(FileStream cach, BinaryWriter writer, float floatNow, int value, UInt16 v0, UInt16 v, UInt16 v1, UInt16 c0, UInt16 c, UInt16 c1)
        {
            UInt64 pos = 0;
            if (v == maxCat)
            {
                //cach.Position = 0 + 16 * ((v1 - 1) * NcVc1H + (c1 - 1)) + 4 * (valueType - 1);
                pos = 4 * crossN + (ulong)4 * v1 * NcVc1H + (ulong)4 * c1;  //not as efficient as v1==0
            }
            if (v1 == maxCat)
            {
                pos = 4 * posLi[v0, v] + (ulong)c0 * NcVc[v] + c;
                //Int64 pos = 4 * ((v0 - 1) * Vc * NcVc0H * NcVcH + (v - 1) * NcVc0H * NcVcH + (c0 - 1) * NcVcH + (c - 1));
            }
            cach.Position = Convert.ToInt64(pos);
            writer.Write(floatNow);
        }


        public float ProbCachGetValue(FileStream cach, BinaryReader reader, int value, UInt16 v0, UInt16 v, UInt16 v1, UInt16 c0, UInt16 c, UInt16 c1)
        {
            float floatNow = 1; UInt64 pos = 0;
            if (v == maxCat)
            {
                //cach.Position = 0 + 16 * ((v1 - 1) * NcVc1H + (c1 - 1)) + 4 * (valueType - 1);
                pos = 4 * crossN + 4 * Convert.ToUInt64(v1) * NcVc1H + 4 * Convert.ToUInt64(c1);  //not as efficient as v1==0
            }
            if (v1 == maxCat)
            {
                pos = 4 * (posLi[v0, v] + (ulong)c0 * NcVc[v] + Convert.ToUInt64(c));
                //Int64 pos = 4 * ((v0 - 1) * Vc * NcVc0H * NcVcH + (v - 1) * NcVc0H * NcVcH + (c0 - 1) * NcVcH + (c - 1));


            }
            cach.Position = Convert.ToInt64(pos);
            floatNow = reader.ReadSingle();
            return floatNow;
        }


    }
}