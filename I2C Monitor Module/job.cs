using System.Collections.Generic;
using System;
using System.Data;

namespace I2C_Monitor_Module
{
	class job //the INI file basically
	{
		public job(List<string> input)
		{
			foreach (string line in input)
				parse(line);
		}//constructor 

		int[] monitor_map = new int[40];
		int pcb_num;
		int bibx = 0;
		int biby = 0;
		int sites;
		ushort slave_add_short;
		string protocol_string;
		string device_name;
		bool extended_bool;
		bool scanned;
		public List<device> device_adds = new List<device>(); // class that fills itself once the string is inserted
		public bool[][] board_list = new bool[16][]; //list of valid boards

		public int Bibx	{get { return this.bibx; } }
		public int Biby { get { return this.biby; } }
		public int[] Monitor_Map{ get { return this.monitor_map; } }
		public bool Scanned { get { return scanned; } set { scanned = value; } }

		private void parse(string line)
		{
			try
			{
				name(line);
				protocol(line);
				extended(line);
				slave_add(line);
				device_add(line);//one for each time device address is listed in file
				pcb(line);
				bib(line); //do both x and y
				monitor(line);
			}
			catch
			{
				throw new InvalidOperationException("Error parsing config file");
			}

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
				slave_add_short = ushort.Parse(line.Replace(token, ""));
		}

		private void device_add(string line)
		{
			string token = "DeviceAddress";
			if (line.Contains(token))
				device_adds.Add(new device(line.Replace("token", "")));
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
	}

	class device //each one of htese is a line in "DeviceAddress"
	{
		public device(string input)
		{
			split = input.Split(',');
			parse_name(split[0]);
			parse_address(split[1]);
			parse_length(split[2]);
			parse_formula(split[3]);
			parse_log(split[4]);
		}
		
		string[] split;
		string name;
		ushort address;
		ushort length;
		string formula;
		bool log;

		public ushort Address { get { return address; } }
		public ushort Length { get { return length; } }

		private void parse_log(string v)
		{
			log = Convert.ToBoolean(short.Parse(v[0].ToString()));
		}

		private void parse_formula(string v)
		{
			formula = v.Substring(v.IndexOf('('), v.Length - v.IndexOf('('));
		}

		private void parse_length(string v)
		{
			length = ushort.Parse(v.Replace("0x", ""));
		}

		private void parse_address(string v)
		{
			int temp = Convert.ToInt32(v.Replace("0x", ""), 16);
			address = ushort.Parse(temp.ToString());
		}

		private void parse_name(string v)
		{
			name = v.Substring(v.IndexOf("\"") + 1, (v.LastIndexOf("\"") - v.IndexOf("\""))-1);
		}
	}
}