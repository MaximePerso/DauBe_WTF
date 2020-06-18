using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Doli.DoPE;
using DauBe_WTF.Utility;
using System.Threading;
using System.Windows.Threading;

namespace DauBe_WTF.ViewModel.SubVM
{
    class DoliVM : VMBase
    {
        #region Private Field with no Property
        #region AutoPos
        #endregion
        #endregion
        #region MVVM AREA
        #region Fields
        #region Doli outputs
        private Dictionary<string, double> _doliData;
        private Double _doliTime;
        private Double _doliLoad;
        private Double _doliPosition;
        private Double _doliExtend;
        #endregion
        #region Doli output data
        private Double _onDataTime;
        private Double _onDataLoad;
        private Double _onDataPosition;
        private Double _onDataExtend;
        private Double _tareTime;
        private Double _tareLoad;
        private Double _tarePosition;
        private Double _tareExtend;
        private Double _xMax;
        private string _display;
        #endregion
        #region Doli Setup
        private DoPE.ERR _error;
        private string _textDisplay;
        private double _defaultVel;
        private double _limLoad;
        private double _velocity;
        private double _destination;
        private DoPE.CTRL _selectedMoveCTRL;
        private bool _isDoliOn;
        #endregion
        #region DoliCTRL
        private DoPE.CTRL _manualCTRL;
        private double _manualDestination;
        #endregion
        #endregion
        #region Properties
        #region Doli outputs
        //public Dictionary<string,double> DoliData
        //{
        //    get => _doliData;
        //    set 
        //    { _doliData = value; OnPropertyChanged("DoliData"); }
        //}
        public double DoliTime
        {
            get => _doliTime;
            set
            { _doliTime = value - _tareTime; OnPropertyChanged("DoliTime"); }
        }
        public double DoliLoad
        {
            get => _doliLoad;
            set
            { _doliLoad = value - _tareLoad; OnPropertyChanged("DoliLoad"); }
        }
        public double DoliPosition
        {
            get => _doliPosition;
            set
            { _doliPosition = value - _tarePosition; OnPropertyChanged("DoliPosition"); }
        }
        public double DoliExtend
        {
            get => _doliExtend;
            set
            { _doliExtend = value - _tareExtend; OnPropertyChanged("DoliExtend"); }
        }
        #endregion
        #region Doli output data
        public double OnDataTime
        {
            get => _onDataTime;
            set
            { _onDataTime = value - _tareTime; OnPropertyChanged("OnDataTime"); }
        }
        public double OnDataLoad
        {
            get => _onDataLoad;
            set
            { _onDataLoad = value - _tareLoad; OnPropertyChanged("OnDataLoad"); }
        }
        public double OnDataPosition
        {
            get => _onDataPosition;
            set
            { _onDataPosition = value - _tarePosition; OnPropertyChanged("OnDataPosition"); }
        }
        public double OnDataExtend
        {
            get => _onDataExtend;
            set
            { _onDataExtend = value - _tareExtend; OnPropertyChanged("OnDataExtend"); }
        }
        public double TareTime
        {
            get => _tareTime;
            set
            { _tareTime = value; OnPropertyChanged("TareTime"); }
        }
        public double TareLoad
        {
            get => _tareLoad;
            set
            { _tareLoad = value; OnPropertyChanged("TareLoad"); }
        }
        public double TarePosition
        {
            get => _tarePosition;
            set
            { _tarePosition = value; OnPropertyChanged("TarePosition"); }
        }
        public double TareExtend
        {
            get => _tareExtend;
            set
            { _tareExtend = value; OnPropertyChanged("TareExtend"); }
        }
        public double XMax
        {
            get => _xMax;
            set
            { _xMax = value; OnPropertyChanged("XMax"); }
        }
        #endregion
        #region Doli setup
        public DoPE.ERR Error
        {
            get => _error;
            set
            { _error = value; OnPropertyChanged("Error"); }
        }
        public bool IsDoliOn
        {
            get => _isDoliOn;
            set 
            { _isDoliOn = value; OnPropertyChanged("IsDoliOn"); }
        }
        public string TextDisplay
        {
            get => _textDisplay;
            set
            { _textDisplay = value; OnPropertyChanged("TextDisplay"); }
        }
        public double DefaultVel
        {
            get => _defaultVel;
            set
            { _defaultVel = value; OnPropertyChanged("DefaultVel"); }
        }
        public double LimLoad
        {
            get => _limLoad;
            set
            { _limLoad = value; OnPropertyChanged("LimLoad"); }
        }
        public double Velocity
        {
            get => _velocity;
            set
            { _velocity = value; OnPropertyChanged("Velocity"); }
        }
        public double Destination
        {
            get => _destination;
            set
            { _destination = value; OnPropertyChanged("Destination"); }
        }
        public DoPE.CTRL SelectedMoveCTRL
        {
            get => _selectedMoveCTRL;
            set
            { _selectedMoveCTRL = value; OnPropertyChanged("SelectedMoveCTRL"); }
        }

        #endregion
        #region DoliCTRL
        public DoPE.CTRL ManualCTRL
        {
            get => _manualCTRL;
            set
            { _manualCTRL = value; OnPropertyChanged("ManualCTRL"); }
        }
        public double ManualDestination
        {
            get => _manualDestination;
            set
            { _manualDestination = value; OnPropertyChanged("ManualDestination"); }
        }
        #endregion
        #endregion
        #region "Static" values
        public string[] DoPEItems { get; set; }
        #endregion
        #region Commands
        public ICommand DoliOnCommand { get; set; }
        public ICommand DoliOffCommand { get; set; }
        public ICommand DoliGoCommand { get; set; }
        #endregion
        #endregion

        #region INITIALISATION
        private readonly int SensorId = 0; //search through all sensor


        /// <summary>
        /// Represents one EDC.
        /// This object is needed to perform DoPE tasks.
        /// (Similar to the DoPE-handle in C++.)
        /// </summary>
        private Edc MyEdc;

        /// <summary>
        /// TAN number assigned to a DoPE command.
        /// (To get informed when a task has been performed.)
        /// </summary>
        private short MyTan;

        /// <summary>
        /// Just an error constant string which is used
        /// when the EDC could not be initialized correctly.
        /// </summary>
        private const string CommandFailedString = "Command failed. Please make sure, that the Edc is successfully initialized. \n";

        /// <summary>
        ///  List of globals to acces measured values
        /// </summary>
        // Loading variables
        public Globals ListData = new Globals();
        //private RealTimeChart NewINstanceOfChart;
        private GraphVM NewINstanceOfChart;
        //private RealTimeCharts NewINstanceOfChart2;
        public delegate void update(double time, double position, double load, double extension);

        public DoliVM()
        {
            Initialisation();
            //ConnectToEdc();
        }

        #region Private methods
        private void Initialisation()
        {
            //Interface parameters
            DoPEItems = Enum.GetNames(typeof(DoPE.CTRL));
            //Doli param
            _isDoliOn = false;
            //Commands ini
            DoliOnCommand = new RelayCommand(o => DoliOn(), o => { return !_isDoliOn; });
            DoliOffCommand = new RelayCommand(o => DoliOff(), o => { return _isDoliOn; });
            DoliGoCommand = new RelayCommand(o => DoliGo(), o => { return (ManualDestination != 0 ); });
            //AutoPosCommand = new RelayCommand();
        }
        #endregion  
        ///----------------------------------------------------------------------
        /// <summary>Connect to EDC</summary>
        ///----------------------------------------------------------------------
        public void ConnectToEdc()
        {
            // tell DoPE which DoPENet.dll and DoPE.dll version we are using
            // THE API CANNOT BE USED WITHOUT THIS CHECK !
            DoPE.CheckApi("2.81");

            try
            {
                DoPE.ERR error;

                // open the first EDC found on this PC
                MyEdc = new Edc(DoPE.OpenBy.DeviceId, SensorId);

                // hang in event-handler to receive DoPE-events
                MyEdc.Eh.OnLineHdlr += new DoPE.OnLineHdlr(OnLine);
#if ONDATABLOCK
        MyEdc.Eh.OnDataBlockHdlr += new DoPE.OnDataBlockHdlr(OnDataBlock);
        // Set number of samples for OnDataBlock events
        // (with 1 ms data refresh rate this leads to a
        //  display refresh every 300 ms)
        error = MyEdc.Eh.SetOnDataBlockSize(300);
        DisplayError(error, "SetOnDataBlockSize");
#else
                MyEdc.Eh.OnDataHdlr += new DoPE.OnDataHdlr(OnData);
#endif
                MyEdc.Eh.OnCommandErrorHdlr += new DoPE.OnCommandErrorHdlr(OnCommandError);
                MyEdc.Eh.OnPosMsgHdlr += new DoPE.OnPosMsgHdlr(OnPosMsg);
                MyEdc.Eh.OnTPosMsgHdlr += new DoPE.OnTPosMsgHdlr(OnTPosMsg);
                MyEdc.Eh.OnLPosMsgHdlr += new DoPE.OnLPosMsgHdlr(OnLPosMsg);
                MyEdc.Eh.OnSftMsgHdlr += new DoPE.OnSftMsgHdlr(OnSftMsg);
                MyEdc.Eh.OnOffsCMsgHdlr += new DoPE.OnOffsCMsgHdlr(OnOffsCMsg);
                MyEdc.Eh.OnCheckMsgHdlr += new DoPE.OnCheckMsgHdlr(OnCheckMsg);
                MyEdc.Eh.OnShieldMsgHdlr += new DoPE.OnShieldMsgHdlr(OnShieldMsg);
                MyEdc.Eh.OnRefSignalMsgHdlr += new DoPE.OnRefSignalMsgHdlr(OnRefSignalMsg);
                MyEdc.Eh.OnSensorMsgHdlr += new DoPE.OnSensorMsgHdlr(OnSensorMsg);
                MyEdc.Eh.OnIoSHaltMsgHdlr += new DoPE.OnIoSHaltMsgHdlr(OnIoSHaltMsg);
                MyEdc.Eh.OnKeyMsgHdlr += new DoPE.OnKeyMsgHdlr(OnKeyMsg);
                MyEdc.Eh.OnRuntimeErrorHdlr += new DoPE.OnRuntimeErrorHdlr(OnRuntimeError);
                MyEdc.Eh.OnOverflowHdlr += new DoPE.OnOverflowHdlr(OnOverflow);
                MyEdc.Eh.OnSystemMsgHdlr += new DoPE.OnSystemMsgHdlr(OnSystemMsg);
                MyEdc.Eh.OnDebugMsgHdlr += new DoPE.OnDebugMsgHdlr(OnDebugMsg);
                MyEdc.Eh.OnRmcEventHdlr += new DoPE.OnRmcEventHdlr(OnRmcEvent);
                MyEdc.Rmc.Enable(-1, -1);

                // Set UserScale
                DoPE.UserScale userScale = new DoPE.UserScale();
                // set position and extension scale to mm
                userScale[DoPE.SENSOR.SENSOR_S] = 1000;
                userScale[DoPE.SENSOR.SENSOR_E] = 1000;

                // Select machine setup and initialize
                error = MyEdc.Setup.SelSetup(DoPE.SETUP_NUMBER.SETUP_1, userScale, ref MyTan, ref MyTan);
                if (error != DoPE.ERR.NOERROR)
                    DisplayError(error, "SelectSetup");
                else
                    Display("SelectSetup : OK !\n");
            }
            catch (DoPEException ex)
            {
                // During the initialization and the
                // shut-down phase a DoPE Exception can arise.
                // Other errors are reported by the DoPE
                // error return codes.
                Display(string.Format("{0}\n", ex));
            }
        }

        #endregion

        #region GUI

        ///----------------------------------------------------------------------
        /// <summary>
        /// Formates and displays DoPE-errors.
        /// </summary>
        /// <param name="error">the dope error to display</param>
        /// <param name="Text">additional text to display</param>
        ///----------------------------------------------------------------------
        private void DisplayError(DoPE.ERR error, string Text)
        {
            if (error != DoPE.ERR.NOERROR)
                Display(Text + " Error: " + error + "\n");
            else if(IsDoliOn == true)
            {
                Display("Doli is ON ! \n");
            }
        }

        ///----------------------------------------------------------------------
        /// <summary>Display debug text</summary>
        ///----------------------------------------------------------------------
        private void Display(string Text)
        {
            TextDisplay += Text;
            Console.WriteLine(TextDisplay);
            //guiDebug.AppendText(Text);
            //guiDebug.UpdateLayout();
        }

        ///----------------------------------------------------------------------
        /// <summary>Activates the EDC's drive.</summary>
        ///----------------------------------------------------------------------
        private void DoliOn()
        {
            IsDoliOn = true;
            try
            {
                DoPE.ERR error = MyEdc.Move.On();
                DisplayError(error, "On");
                var emergency = new Emergency(MyEdc, MyTan);
                //emergency.Show();
            }
            catch (NullReferenceException)
            {
                Display(CommandFailedString);
            }
        }

        ///----------------------------------------------------------------------
        /// <summary>Deactivates the EDC's drive.</summary>
        ///----------------------------------------------------------------------
        private void DoliOff()
        {
            IsDoliOn = false;
            try
            {
                DoPE.ERR error = MyEdc.Move.Off();
                DisplayError(error, "Off");
            }
            catch (NullReferenceException)
            {
                Display(CommandFailedString);
            }
        }

        #endregion

        #region DoPE Events
        private Int32 LastTime = Environment.TickCount;

        private int OnData(ref DoPE.OnData Data, object Parameter)
        {
            DoPE.Data Sample = Data.Data;

            // Live data
            ListData.OnDataTime = Sample.Time;
            ListData.OnDataPosition = Sample.Sensor[(int)DoPE.SENSOR.SENSOR_S];
            ListData.OnDataLoad = Sample.Sensor[(int)DoPE.SENSOR.SENSOR_F];
            ListData.OnDataExtend = Sample.Sensor[(int)DoPE.SENSOR.SENSOR_E];

            if (Data.DoPError == DoPE.ERR.NOERROR)
            {
                //button1.Click += (Data2, Parameter2) => Myevent(Sample, Parameter2);
                Int32 Time = Environment.TickCount;
                if ((Time - LastTime) >= 250 /*ms*/)
                {

                    // Send the data from the ondata handler inside of a global list
                    ListData.time.Add(Sample.Time);
                    ListData.position.Add(Sample.Sensor[(int)DoPE.SENSOR.SENSOR_S]);
                    ListData.load.Add(Sample.Sensor[(int)DoPE.SENSOR.SENSOR_F]);
                    ListData.extend.Add(Sample.Sensor[(int)DoPE.SENSOR.SENSOR_E]);

                    //update pass = new update(NewINstanceOfChart.UpdateValues);
                    //pass(ListData.time.Last(), ListData.position.Last(), ListData.load.Last(), ListData.extend.Last());

                    LastTime = Time;

                    //il faudrait utiliser une property dependency pour optimiser l'utilisation de la mémoire
                    DoliTime = Sample.Time;

                    DoliPosition = Sample.Sensor[(int)DoPE.SENSOR.SENSOR_S];

                    DoliLoad = Sample.Sensor[(int)DoPE.SENSOR.SENSOR_F];

                    DoliExtend = Sample.Sensor[(int)DoPE.SENSOR.SENSOR_E];
                }
            }
            return 0;
        }

        //public static bool FormIsOpen(FormCollection application, Type formType)
        //{
        //    return Application.OpenForms.Cast<Form>().Any(openForm => openForm.GetType)
        //}

        private int OnLine(DoPE.LineState LineState, object Parameter)
        {
            Display(string.Format("OnLine: {0}\n", LineState));

            return 0;
        }

        private int OnCommandError(ref DoPE.OnCommandError CommandError, object Parameter)
        {
            Display(string.Format("OnCommandError: CommandNumber={0} ErrorNumber={1} usTAN={2} \n",
              CommandError.CommandNumber, CommandError.ErrorNumber, CommandError.usTAN));

            return 0;
        }

        private int OnPosMsg(ref DoPE.OnPosMsg PosMsg, object Parameter)
        {
            Display(string.Format("OnPosMsg: DoPError={0} Reached={1} Time={2} Control={3} Position={4} DControl={5} Destination={6} usTAN={7} \n",
              PosMsg.DoPError, PosMsg.Reached, PosMsg.Time, PosMsg.Control, PosMsg.Position, PosMsg.DControl, PosMsg.Destination, PosMsg.usTAN));
            // get the control mode defined in the dropping menu
            DoPE.CTRL control = SelectedMoveCTRL;
            // if current control mode is position AND the limit load is reached, proc that message
            if ((control == DoPE.CTRL.POS) & ((Math.Abs(PosMsg.Destination) > Math.Abs(LimLoad) * 0.90)))
            {
                MessageBox.Show("ERROR: you exceed 90% of the defined limit load");
            }

            return 0;
        }

        private int OnTPosMsg(ref DoPE.OnPosMsg PosMsg, object Parameter)
        {
            Display(string.Format("OnTPosMsg: DoPError={0} Reached={1} Time={2} Control={3} Position={4} DControl={5} Destination={6} usTAN={7} \n",
              PosMsg.DoPError, PosMsg.Reached, PosMsg.Time, PosMsg.Control, PosMsg.Position, PosMsg.DControl, PosMsg.Destination, PosMsg.usTAN));

            return 0;
        }

        private int OnLPosMsg(ref DoPE.OnPosMsg PosMsg, object Parameter)
        {
            Display(string.Format("OnLPosMsg: DoPError={0} Reached={1} Time={2} Control={3} Position={4} DControl={5} Destination={6} usTAN={7} \n",
              PosMsg.DoPError, PosMsg.Reached, PosMsg.Time, PosMsg.Control, PosMsg.Position, PosMsg.DControl, PosMsg.Destination, PosMsg.usTAN));

            return 0;
        }

        private int OnSftMsg(ref DoPE.OnSftMsg SftMsg, object Parameter)
        {
            Display(string.Format("OnSftMsg: DoPError={0} Upper={1} Time={2} Control={3} Position={4} usTAN={5} \n",
              SftMsg.DoPError, SftMsg.Upper, SftMsg.Time, SftMsg.Control, SftMsg.Position, SftMsg.usTAN));
            // Gentle message to inform the user he may have fucked up
            MessageBox.Show("TU AS MERDE MAURICE, RETOUR A LA VALEUR DE PRESSION LIMITE");
            // The following moves the x-head to lower the load in case the user went to far. SftMsg.Position actually stores the load ...
            resetSft();
            double endDest = Math.Abs(LimLoad);
            if (SftMsg.Position < 0.0)
            {
                Console.WriteLine("negatif = " + -endDest);
                MyEdc.Move.Pos(DoPE.CTRL.LOAD, 500, -endDest, ref MyTan);
            }
            else
            {
                Console.WriteLine("positif = " + endDest);
                MyEdc.Move.Pos(DoPE.CTRL.LOAD, 0.1, endDest, ref MyTan);
            }
            return 0;
        }

        private int OnOffsCMsg(ref DoPE.OnOffsCMsg OffsCMsg, object Parameter)
        {
            Display(string.Format("OnOffsCMsg: DoPError={0} Time={1} Offset={2} usTAN={3} \n",
              OffsCMsg.DoPError, OffsCMsg.Time, OffsCMsg.Offset, OffsCMsg.usTAN));

            return 0;
        }

        private int OnCheckMsg(ref DoPE.OnCheckMsg CheckMsg, object Parameter)
        {
            Display(string.Format("OnCheckMsg: DoPError={0} Action={1} Time={2} CheckId={3} Position={4} SensorNo={5} usTAN={6} \n",
              CheckMsg.DoPError, CheckMsg.Action, CheckMsg.Time, CheckMsg.CheckId, CheckMsg.Position, CheckMsg.SensorNo, CheckMsg.usTAN));

            return 0;
        }

        private int OnShieldMsg(ref DoPE.OnShieldMsg ShieldMsg, object Parameter)
        {
            Display(string.Format("OnShieldMsg: DoPError={0} Action={1} Time={2} SensorNo={3} Position={4} usTAN={5} \n",
              ShieldMsg.DoPError, ShieldMsg.Action, ShieldMsg.Time, ShieldMsg.SensorNo, ShieldMsg.Position, ShieldMsg.usTAN));

            return 0;
        }

        private int OnRefSignalMsg(ref DoPE.OnRefSignalMsg RefSignalMsg, object Parameter)
        {
            Display(string.Format("OnRefSignalMsg: DoPError={0} Time={1} SensorNo={2} Position={3} usTAN={4} \n",
              RefSignalMsg.DoPError, RefSignalMsg.Time, RefSignalMsg.SensorNo, RefSignalMsg.Position, RefSignalMsg.usTAN));

            return 0;
        }

        private int OnSensorMsg(ref DoPE.OnSensorMsg SensorMsg, object Parameter)
        {
            Display(string.Format("OnSensorMsg: DoPError={0} Time={1} SensorNo={2} usTAN={3} \n",
              SensorMsg.DoPError, SensorMsg.Time, SensorMsg.SensorNo, SensorMsg.usTAN));

            return 0;
        }

        private int OnIoSHaltMsg(ref DoPE.OnIoSHaltMsg IoSHaltMsg, object Parameter)
        {
            Display(string.Format("OnIoSHaltMsg: DoPError={0} Upper={1} Time={2} Control={3} Position={4} usTAN={5} \n",
              IoSHaltMsg.DoPError, IoSHaltMsg.Upper, IoSHaltMsg.Time, IoSHaltMsg.Control, IoSHaltMsg.Position, IoSHaltMsg.usTAN));

            return 0;
        }

        private int OnKeyMsg(ref DoPE.OnKeyMsg KeyMsg, object Parameter)
        {
            Display(string.Format("OnKeyMsg: DoPError={0} Time={1} Keys={2} NewKeys={3} GoneKeys={4} OemKeys={5} NewOemKeys={6} GoneOemKeys={7} usTAN={8} \n",
              KeyMsg.DoPError, KeyMsg.Time, KeyMsg.Keys, KeyMsg.NewKeys, KeyMsg.GoneKeys, KeyMsg.OemKeys, KeyMsg.NewOemKeys, KeyMsg.GoneOemKeys, KeyMsg.usTAN));

            return 0;
        }

        private int OnRuntimeError(ref DoPE.OnRuntimeError RuntimeError, object Parameter)
        {
            Display(string.Format("OnRuntimeError: DoPError={0} ErrorNumber={1} Time={2} Device={3} Bits={4} usTAN={5} \n",
              RuntimeError.DoPError, RuntimeError.ErrorNumber, RuntimeError.Time, RuntimeError.Device, RuntimeError.Bits, RuntimeError.usTAN));

            return 0;
        }

        private int OnOverflow(int Overflow, object Parameter)
        {
            Display(string.Format("OnOverflow: Overflow={0} \n", Overflow));

            return 0;
        }

        private int OnDebugMsg(ref DoPE.OnDebugMsg DebugMsg, object Parameter)
        {
            Display(string.Format("OnDebugMsg: DoPError={0} MsgType={1} Time={2} Text={3} \n",
              DebugMsg.DoPError, DebugMsg.MsgType, DebugMsg.Time, DebugMsg.Text));

            return 0;
        }

        private int OnSystemMsg(ref DoPE.OnSystemMsg SystemMsg, object Parameter)
        {
            Display(string.Format("OnSystemMsg: DoPError={0} MsgNumber={1} Time={2} Text={3} \n",
              SystemMsg.DoPError, SystemMsg.MsgNumber, SystemMsg.Time, SystemMsg.Text));

            return 0;
        }

        private int OnRmcEvent(ref DoPE.OnRmcEvent RmcEvent, object Parameter)
        {
            Display(string.Format("OnRmcEvent: Keys={0} NewKeys={1} GoneKeys={2} Leds={3} NewLeds={4} GoneLeds={5} \n",
              RmcEvent.Keys, RmcEvent.NewKeys, RmcEvent.GoneKeys, RmcEvent.Leds, RmcEvent.NewLeds, RmcEvent.GoneLeds));

            return 0;
        }


        #endregion

        #region TEST CODE
        private void guiControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            DoPE.CTRL control = SelectedMoveCTRL;
            /*
            switch (control)
            {
                case DoPE.CTRL.POS:
                    lblUnits.Content = "mm";
                    lblDestination.Content = "Position";
                    velUnits.Content = "mm/s";
                    /// reset values to limit missclicks
                    destination.Text = "0";
                    break;
                case DoPE.CTRL.LOAD:
                    lblUnits.Content = "N";
                    lblDestination.Content = "Load";
                    velUnits.Content = "N/s";
                    /// reset values to limit missclicks
                    destination.Text = "0";
                    break;
                case DoPE.CTRL.EXTENSION:
                    lblUnits.Content = "mm";
                    lblDestination.Content = "Extension";
                    /// reset values to limit missclicks
                    destination.Text = "0";
                    break;
                default:
                    lblUnits.Content = "mm";
                    lblDestination.Content = "Position";
                    velUnits.Content = "mm/s";
                    /// reset values to limit missclicks
                    destination.Text = "0";
                    break;
            }*/
        }
        private double noProblemJeanClaude(String chiant)
        {
            double moinsChiant;

            if (chiant.Contains(".")) { moinsChiant = Convert.ToDouble(chiant.Replace(".", ",")); }
            else { moinsChiant = Convert.ToDouble(chiant); }

            return moinsChiant;
        }

        private void Myevent(DoPE.Data MyData, object Parameter)
        {
            Display(MyData.Sensor[0].ToString());

        }

        private void moveUp()
        {
            double speed = Math.Abs(DefaultVel);
            Int32 i = MyEdc.DoPEDllHdl;
            DoPE.ERR error = MyEdc.Move.FMove(DoPE.MOVE.UP, DoPE.CTRL.POS, speed, ref MyTan);

            //Setup security load
            double uprLim = Math.Abs(LimLoad);
            double lwrLim = -1.0 * Math.Abs(LimLoad);
            // When pressing up button, only risk is to apply to much tensile load. However in the case the user went to far while applying a
            // pressure load, if uprLim == -lwrLim, the error message saying to much load is applied will be triggered the first time this 
            // button is used. Multiplying it by five gives a load buffer to avoid that message. Keep in mind, there is still the max load defined
            // in the DOLI settings that prevails over everything.
            DoPE.ERR error2 = MyEdc.Ctrl.Sft(DoPE.CTRL.LOAD, uprLim, lwrLim * 5.0, DoPE.REACT.ACTION);
        }

        private void moveDown()
        {
            double speed = Math.Abs(DefaultVel);
            Int32 i = MyEdc.DoPEDllHdl;
            DoPE.ERR error = MyEdc.Move.FMove(DoPE.MOVE.DOWN, DoPE.CTRL.POS, speed, ref MyTan);

            //Setup security load
            double uprLim = Math.Abs(LimLoad);
            double lwrLim = -1.0 * Math.Abs(LimLoad);
            // When pressing down button, only risk is to apply to much pressure. However in the case the user went to far while applying a
            // tensile load, if uprLim == -lwrLim, the error message saying to much load is applied will be triggered the first time this 
            // button is used. Multiplying it by five gives a load buffer to avoid that message. Keep in mind, there is still the max load defined
            // in the DOLI settings that prevails over everything.
            DoPE.ERR error2 = MyEdc.Ctrl.Sft(DoPE.CTRL.LOAD, uprLim * 5.0, lwrLim, DoPE.REACT.ACTION);
        }

        private void stop()
        {
            DoPE.ERR error = MyEdc.Move.Halt(DoPE.CTRL.POS, ref MyTan);
            // resets the sorftend limit in case it has been reached
            resetSft();
        }

        private void resetSft()
        // function used to reset softend for the user to be able to send other command from the window
        {
            double uprLim = Math.Abs(LimLoad) * 5.0;
            double lwrLim = -1.0 * Math.Abs(LimLoad) * 5.0;
            DoPE.ERR error2 = MyEdc.Ctrl.Sft(DoPE.CTRL.LOAD, uprLim, lwrLim, DoPE.REACT.STATUS);
        }

        public void moveToDest(DoPE.CTRL controlMove, double destination, double velLim = 1.0, double limit = 0.0, short yourRef = 0, int flag = 0)
        {
            // in case the user did not specify a speed
            if (Velocity == 0)
            {
                Velocity = 1.0;
            }
            velLim = Velocity;
            limit = -Math.Abs(LimLoad);
            // distinction required, otherwise the X-head only move in one given direction
            if (destination > ListData.OnDataPosition)
            {
                limit = -limit;
            }
            DoPE.ERR error;
            //    controlMove, decDest, destination, DoPE.DESTMODE.DEST_POSITION, ref MyTan);
            // Special move in case position is used for destination. It fixes a limit load to avoid problems
            if (controlMove == DoPE.CTRL.POS)
            {
                // in this command, the limit and the destination have been inverted so the x-head is piloted using movement speed instead of loading speed (much
                // faster). However, it means that if the limit load is reached, OnPosMsg will proc instead of OnLPosMsg
                error = MyEdc.Move.PosExt(controlMove, velLim, DoPE.LIMITMODE.ABSOLUTE, destination, DoPE.CTRL.LOAD, limit, DoPE.DESTMODE.APPROACH, ref MyTan);
            }
            else
            {
                //MessageBox.Show("controlMove " + controlMove.ToString() + ", velocity " + velLim.ToString() + ", destination " + destination.ToString() + ", MyTan " + MyTan.ToString());           
                error = MyEdc.Move.Pos(controlMove, velLim, destination, ref MyTan);
            }
            Display(error.ToString());
        }

        private void ResetList()
        {
            ListData.time.Clear();
            ListData.position.Clear();
            ListData.load.Clear();
            ListData.extend.Clear();
        }

        private void UpdateChart()
        {

        }
        #endregion

        #region BUTTON ACTION

        private void upBut_MouseDown(object sender, MouseEventArgs e)
        {
            // in case the user did not specify a speed
            if (DefaultVel == 0)
            {
                DefaultVel = 1.0;
            }
            moveUp();
        }

        private void _MouseUp(object sender, MouseEventArgs e)
        {
            stop();
        }

        private void downBut_MouseDown(object sender, EventArgs e)
        {
            // in case the user did not specify a speed
            if (DefaultVel == 0)
            {
                DefaultVel = 1.0;
            }
            moveDown();
        }

        #endregion

        private void DoliGo()
        {
            double dest = Destination;
            DoPE.CTRL CTRL = SelectedMoveCTRL;
            moveToDest(CTRL, dest);
        }

        public async Task AsyncAutoPosApproach()
        {
            double velLim = 100; //Deplacement de la X-head à 100 mm/s
            double lim = -100; //Limite de position (mm) qui ne sera jamais atteinte, mais demandée par PosExt
            double destination = -1000; //Destination visée par la Xhead -1000N (compression)
            //On déplace le piston à 100mm/s jusqu'à ce qu'une force de -1000N soit enregistrée (ou que sa position soit arrivée à -100 mm)
            Error = MyEdc.Move.PosExt(DoPE.CTRL.POS, velLim, DoPE.LIMITMODE.ABSOLUTE, destination, DoPE.CTRL.LOAD, lim, DoPE.DESTMODE.APPROACH, ref MyTan);
            await Task.Run(() =>
            {
                while (Math.Abs(DoliLoad) < Math.Abs(lim) * 0.8)
                {
                    System.Threading.Thread.Sleep(250);
                }
            });
        }

        public async Task AsyncAutoPosBallRelease()
        {
            double offset = 20.0; // offset permettant de remonter la Xhead de 20mm
            double lim = 150; 
            double destination = DoliPosition + offset;
            double velLim = 100;
            //On remonte simplement le piston de 20mm
            Error = MyEdc.Move.PosExt(DoPE.CTRL.POS, velLim, DoPE.LIMITMODE.ABSOLUTE, destination, DoPE.CTRL.LOAD, lim, DoPE.DESTMODE.APPROACH, ref MyTan);
            await Task.Run(() =>
            {
                while (Math.Abs(Math.Abs(DoliPosition) - Math.Abs(destination)) > 0.2)
                {
                    System.Threading.Thread.Sleep(250);
                }
            });
        }

        public async Task AsyncAutoPosFinal(double Progression)
        {
            double squishedBall = 25;
            double lim = -100;
            double destination = DoliPosition - squishedBall;
            double velLim = 5;
            Progression = 0;
            // On replace le piston à sa place basse
            Error = MyEdc.Move.PosExt(DoPE.CTRL.POS, velLim, DoPE.LIMITMODE.ABSOLUTE, destination, DoPE.CTRL.LOAD, lim, DoPE.DESTMODE.APPROACH, ref MyTan);
            await Task.Run(() =>
            {
                while (Math.Round(DoliPosition,2) != Math.Round(destination,2))
                {
                    Progression = Math.Round((1 - (Math.Abs(Math.Abs(DoliPosition - destination)) / squishedBall) * 100),1);
                    System.Threading.Thread.Sleep(25);
                }
            });
        }

        public async Task pouetpouet()
        {
            await Task.Delay(2000);
        }


        private void btnRecord_Click(object sender, EventArgs e)
        {

            ////Wipe de list to start frech recording
            //ResetList();
        }


        private void btnGraph_Click(object sender, EventArgs e)
        {

            //var thread = new Thread(() =>
            //{
            ////    var liveChart = new RealTimeChart(ListData);
            ////    Application.Run(liveChart);

            //    NewINstanceOfChart = new RealTimeChart(ListData);
            //    NewINstanceOfChart.Show();
            //});
            ////thread.SetApartmentState(ApartmentState.STA);
            //thread.Start();
            ////NewINstanceOfChart = new RealTimeChart(ListData);
            ////NewINstanceOfChart.Show();
        }

        private void btnPos_Click(object sender, EventArgs e)
        {
            //var calibration = new Calibration(MyEdc, MyTan, ListData);
            //calibration.Show();
        }

        private void btnGraph_Click(object sender, RoutedEventArgs e)
        {
            //NewINstanceOfChart2 = new RealTimeCharts();
            //NewINstanceOfChart2.Show();
            //    var thread = new Thread(() =>
            //    {
            //        NewINstanceOfChart2 = new RealTimeCharts();
            //        NewINstanceOfChart2.Show();

            //        // to ensure no ghost thread is left when closing the program
            //        NewINstanceOfChart2.Closed += (sendr2, e2) =>
            //        NewINstanceOfChart2.Dispatcher.InvokeShutdown();
            //    Dispatcher.Run();
            //});

            //    thread.SetApartmentState(ApartmentState.STA);
            //    thread.Start();
            //    //realTimeChart.ShowDialog();
        }

        private void guiOn(object sender, RoutedEventArgs e)
        {

        }



    }
}


