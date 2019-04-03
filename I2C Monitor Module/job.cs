using System.Collections.Generic;
using System;
using System.Data;

namespace I2C_Monitor_Module
{
	class job
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
		byte slave_add_short;
		string protocol_string;
		string device_name;
		bool extended_bool;
		List<device> device_adds = new List<device>(); // class that fills itself once the string is inserted

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
			string token = "SlaveAddress=";
			if (line.Contains(token))
				slave_add_short = byte.Parse(line.Replace(token, ""));
		}

		private void device_add(string line)
		{
			string token = "DeviceAddress";
			if (line.Contains(token))
				device_adds.Add(line.Replace(token, ""));
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
				bibx = Int32.Parse(line.Replace(token + "X", ""));
			if (line.Contains(token + "Y"))
				biby = Int32.Parse(line.Replace(token + "Y", ""));
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

		private void resolve_addresses()
		{
			for(int i = 0; i < device_adds.Count; i++)
			{
				string current = device_adds[i].Substring(device_adds[i].IndexOf('('), device_adds[i].Length - device_adds[i].IndexOf(')')); 
				DataTable table = new DataTable();
				device_adds[i] = (string)table.Compute(current, string.Empty);
			}
		}
	}

	class device
	{
		public device(string input)
		{
			this.input = input.Split(',');
		}

		string[] input;
		string name;
		byte address;
		ushort length;
		string formula;


	}
}