using System.Collections.Generic;
using System;
using System.Data;
using System.Linq;

namespace I2C_Monitor_Module
{
    public class job //the INI file basically
    {
        public job(List<string> input)
        {
            file_contents = input;
            for (int i = 0; i < 40; i++) monitor_map[i] = (i+1); //fill this up first
            foreach (string line in input)
                parse(line);
            sort_adds();
        }//constructor 

        public List<string> file_contents;
        int[] monitor_map = new int[40];
        int pcb_num;
        int bibx = 0;
        int biby = 0;
        int sites;
        int log_interval = 5;
        ushort slave_add_short;
        ushort read_address;
        string protocol_string;
        string device_name;
        string logfile_name;
        string logfile_path = "C://InSituMonitorModule//ReadLogs//";

        bool extended_bool;
        bool scanned;
        public List<device> device_adds = new List<device>(); // class that fills itself once the string is inserted
        public device current_adds;
        //these next 5 should probably be their own class -> future improvement
        public bool[][] board_list = new bool[16][]; //list of valid boards
        public log[][] board_log = new log[16][]; //storing info for logging
        public int[][][] board_retries = new int[16][][]; //for storing retries (boards, duts, addresses)
		public string[] board_names = new string[16]; //storing names
		public page[] board_pages = new page[16]; //contains labels, tooltips, etc
		public values board_values;
        public List<int> tab_page_map = new List<int>();

        public int Bibx { get => bibx; }
        public int Biby { get => biby; }
        public int[] Monitor_Map { get => monitor_map; }
        public bool Scanned { get => scanned; set => scanned = value; }
        public bool Extended { get => extended_bool; }
        public int Sites { get => sites; }
        public int LogInterval { get => log_interval; }
        public string LogFileName { get => logfile_name; set => logfile_name = value; }
        public string LogFilePath { get => logfile_path; set => logfile_path = value; }
        public ushort ReadAddress { get => read_address; }
        private void parse(string line)
        {
            try
            {
                name(line);
                protocol(line);
                extended(line);
                slave_add(line);
                read_add(line);
                device_add(line);//one for each time device address is listed in file
                pcb(line);
                bib(line); //do both x and y
                monitor(line);
                interval(line);
                logfile(line);
            }
            catch
            {
                throw new InvalidOperationException("Error parsing config file");
            }

        }

        private void logfile(string line)
        {
            string token = "LogFileName=";
            if (line.Contains(token))
            {
                logfile_name = line.Replace(token, "");
                logfile_name = logfile_name.Replace("\'", "");
            }
        }

        private void interval(string line)
        {
            string token = "LogInterval=";
            if (line.Contains(token))
                Int32.TryParse(line.Replace(token, ""), out log_interval);
        }

        private void name(string line)
        {
            string token = "DeviceName=";
            if (line.Contains(token))
                device_name = line.Replace(token, "");
        }

        private void protocol(string line)
        {
            string token = "Protocol=";
            if (line.Contains(token))
                protocol_string = line.Replace(token, "");
        }

        private void extended(string line)
        {
            string token = "ExtendedAddress=";
            if (line.Contains(token))
                extended_bool = bool.Parse(line.Replace(token, ""));
        }

        private void slave_add(string line)
        {
            string token = "SlaveAddress=0x";
            if (line.Contains(token))
                slave_add_short = ushort.Parse(line.Replace(token, ""), System.Globalization.NumberStyles.HexNumber);
        }

        private void read_add(string line)
        {
            string token = "ReadAddress=0x";
            if (line.Contains(token))
                read_address = ushort.Parse(line.Replace(token, ""), System.Globalization.NumberStyles.HexNumber);

        }

		private void device_add(string line)
		{
			string token = "DeviceAddress";
			if (line.Contains(token))
				device_adds.Add(new device(line.Replace("token", ""), extended_bool));
			//this po p ulates the device addresses, need to then parse further
		}

		private void pcb(string line)
		{
			string token = "PCB=";
			if (line.Contains(token))
				pcb_num = Int32.Parse(line.Replace(token, ""));
		}

		private void bib(string line)
		{
			string token = "Bib";
			if (line.Contains(token + "X"))
				bibx = Int32.Parse(line.Replace(token + "X=", ""));
			if (line.Contains(token + "Y"))
				biby = Int32.Parse(line.Replace(token + "Y=", ""));
			if (bibx != 0 && biby != 0) //if both are filled
				sites = bibx * biby;
		}

		private void monitor(string line)
		{
			string token = "MonitorMap=(";
			if (line.Contains(token))
			{
				line = line.Replace(token, "");
				line = line.Remove(line.IndexOf(")"), 1);
				var monitors = line.Split(',');
				for(int i = 0; i < monitors.Length; i++)
				{
					if (!Int32.TryParse(monitors[i], out monitor_map[i])) //do the conversion, if it fails (like a letter, or blnak)
						continue; //skip this iteration then. int array will be empty
				}
			}
		}

        private void sort_adds()
        {
            var list = device_adds.OrderBy(o => o.LogOrder).ToList();
            current_adds = device_adds[0]; //to start off
        }
	}

	public class device //each one of htese is a line in "DeviceAddress"
	{
		public device(string input, bool extended)
		{
			split = input.Split(',');
			parse_name(split[0]);
			parse_address(split[1], extended);
			parse_length(split[2]);
			parse_formula(split[3]);
			parse_log(split[4]);
		}
		
		string[] split;
		string name;
		ushort[] address = new ushort[2];
		ushort length;
		string formula;
		ushort log_order;
        bool literal;

		public ushort[] Address { get => address; }
		public ushort Length { get => length; }
        public string Forumla { get => formula; }
        public string Name { get => name; }
        public ushort LogOrder { get => log_order; }
		public float Low { get; set; }
		public float High { get; set; }
        public bool Literal { get => literal; }

		private void parse_log(string v)
		{
			log_order = ushort.Parse(v[0].ToString());
		}

		private void parse_formula(string v)
		{
			formula = v.Substring(v.IndexOf('('), v.Length - v.IndexOf('('));
            if (formula == "ReadValue" || formula == "(ReadValue)")
                literal = true;
            else
                literal = false;
		}

		private void parse_length(string v)
		{
			length = ushort.Parse(v.Replace("0x", ""));
		}

		private void parse_address(string v, bool extended)
		{
            if (extended)
            {
                string trim = v.Replace("0x", "");
                int temp1 = Convert.ToInt32(new string(new char[] { trim[0], trim[1] }), 16); //split based on char index
                int temp2 = Convert.ToInt32(new string(new char[] { trim[2], trim[3] }), 16);
                address[0] = ushort.Parse(temp1.ToString());
                address[1] = ushort.Parse(temp2.ToString());
            }
            else
            {
                int temp = Convert.ToInt32(v.Replace("0x", ""), 16);
                address[0] = ushort.Parse(temp.ToString());
            }
		}

		private void parse_name(string v)
		{
			name = v.Substring(v.IndexOf("\"") + 1, (v.LastIndexOf("\"") - v.IndexOf("\""))-1);
		}
	}
}