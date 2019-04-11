using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace I2C_Monitor_Module
{
	class TotalPhase
	{
		public List<Aardvark> aardvarks = new List<Aardvark>();
		public List<Beagle> beagles = new List<Beagle>();

		public Aardvark current_aardvark = new Aardvark();
		public Beagle current_beagle = new Beagle();
		public job current_job;

		public void find_device()
		{
			int device_num = 8;
			var ports = Enumerable.Repeat<ushort>(9999, device_num).ToArray();
			var ids = new uint[device_num];

			try
			{
				int aa_num = AardvarkApi.aa_find_devices_ext(device_num, ports, device_num, ids);
				if (aa_num > 0)
				{
					for (int i = 0; i < device_num; i++)
						if (ports[i] != 9999)
							aardvarks.Add(new Aardvark(ports[i], ids[i]));
				}
				int bg_num = BeagleApi.bg_find_devices_ext(device_num, ports, device_num, ids);
				if (bg_num > 0)
				{
					for (int i = 0; i < device_num; i++)
						if (ports[i] != 9999)
							beagles.Add(new Beagle(ports[i], ids[i]));
				}
			}
			catch (System.TypeInitializationException)
			{
				MessageBox.Show("Application must target 64 bit");
			}
		}
	}

	class Aardvark
	{
		public Aardvark()
		{
			port = 999;
		} //for blank one

		public Aardvark(ushort port, uint id)
		{
			this.port = port;
			this.id = id;
			open_handle();
			setup_i2c();
		} //for building a new one

		ushort port;
		uint id;
		int handle;
		int bitrate = 100;
		int bus_timeout;
		public const int BUS_TIMEOUT = 150;  // ms

		public ushort Port { get { return port; } }
		public uint ID { get { return id; } }

		public int i2c_write(ushort slave_addr, ushort num_bytes, byte[] data_out)
		{
			//only for SHT module
			var result = AardvarkApi.aa_i2c_write(handle, slave_addr, AardvarkI2cFlags.AA_I2C_NO_FLAGS, num_bytes, data_out);

			if (result < 0)
				Console.WriteLine("error: {0}\n", AardvarkApi.aa_status_string(result));

			return result;
		}

		public byte[] i2c_read(ushort slave_addr, ushort bytes, out byte[] data_in)
		{
			data_in = Enumerable.Repeat<byte>(0, bytes).ToArray(); //set it to all 0's first

			var result = AardvarkApi.aa_i2c_read(handle, slave_addr, AardvarkI2cFlags.AA_I2C_NO_FLAGS, bytes, data_in);

			if (result < 0)
				Console.WriteLine("error: {0}\n", AardvarkApi.aa_status_string(result));

			return data_in;
		}

		private void open_handle()
		{
			handle = AardvarkApi.aa_open(port);
			if (handle <= 0)
			{
				Console.WriteLine("Unable to open Aardvark device on port {0}", port);
				Console.WriteLine("error: {0}", AardvarkApi.aa_status_string(handle));
			}
		}

		private void setup_i2c()
		{
			// Ensure that the I2C subsystem is enabled
			AardvarkApi.aa_configure(handle, AardvarkConfig.AA_CONFIG_SPI_I2C);

			// Enable the I2C bus pullup resistors (2.2k resistors).
			// This command is only effective on v2.0 hardware or greater.
			// The pullup resistors on the v1.02 hardware are enabled by default.
			AardvarkApi.aa_i2c_pullup(handle, AardvarkApi.AA_I2C_PULLUP_BOTH);

			// Power the EEPROM using the Aardvark adapter's power supply.
			// This command is only effective on v2.0 hardware or greater.
			// The power pins on the v1.02 hardware are not enabled by default.
			AardvarkApi.aa_target_power(handle, AardvarkApi.AA_TARGET_POWER_BOTH);

			// Set the bitrate
			bitrate = AardvarkApi.aa_i2c_bitrate(handle, bitrate);
			Console.WriteLine("Bitrate set to {0} kHz", bitrate);

			// Set the bus lock timeout
			bus_timeout = AardvarkApi.aa_i2c_bus_timeout(handle, BUS_TIMEOUT);
			Console.WriteLine("Bus lock timeout set to {0} ms", bus_timeout);
		}
	}

	class Beagle
	{
		public Beagle()
		{
		} //for blank one

		public Beagle(ushort port, uint id)
		{
			this.port = port;
			this.id = id;
			open_handle();
			setup_i2c();
		}

		const int max_bytes = 3;  //6 for SHT31
		ushort port;
		uint id;
		int handle;
		int sample_rate;
		int timeout;
		int buffer_available;
		ushort[] data_in = new ushort[max_bytes];
		uint[] timing;
		int timing_size;

        public List<ushort> buffer = new List<ushort>();

		public ushort Port{get { return port; }	}
		public uint ID { get { return id; } }
		public ushort[] Data { get { return data_in; } }
		public int Buffer_Free{ get { return buffer_available; }	}

		public void reset_beagle()
		{
			BeagleApi.bg_disable(handle);
			System.Threading.Thread.Sleep(250);
			setup_i2c();
		}

        public void clear_buffer()
        {
            data_in = new ushort[max_bytes];
            buffer.Clear();
        }

		private void open_handle()
		{
			handle = BeagleApi.bg_open(port);
			if (handle <= 0)
			{
				Console.WriteLine("Unable to open Beagle device on port {0}", port);
				Console.WriteLine("error: {0}", BeagleApi.bg_status_string(handle));
			}
		}

		private void setup_i2c() //set timing bit size, sample rate, and timeout
		{
			// Get the size of the timing information for a transaction of
			// max_bytes length
			timing_size = BeagleApi.bg_bit_timing_size(BeagleProtocol.BG_PROTOCOL_I2C, max_bytes);
			timing = new uint[timing_size];

			sample_rate = BeagleApi.bg_samplerate(handle, 50000); //sampling rate in khz
			if (BeagleApi.bg_timeout(handle, 1000) != (int)BeagleStatus.BG_OK) //set the timeout to 1s
			{
				Console.WriteLine("error: Could not set Beagle timeout; exiting...\n");
				throw new InvalidOperationException("Could not set Beagle timeout; exiting...");
			}
			if (BeagleApi.bg_enable(handle, BeagleProtocol.BG_PROTOCOL_I2C) != (int)BeagleStatus.BG_OK) //start polling bus
			{
				Console.WriteLine("error: could not enable I2C capture; exiting...\n");
				throw new InvalidOperationException("error: could not enable I2C capture; exiting...");
			}
		}

		public List<string> snoop_i2c(int num_packets)
		{
			// Capture and print each transaction
			List<ushort> full_data = new List<ushort>(); //keep adding bytes to this as it goes
			List<string> lines = new List<string>();
			for (int i = 0; i < num_packets || num_packets == 0; ++i)
			{
				uint status = 0;
				ulong time_sop = 0, time_sop_ns = 0;
				ulong time_duration = 0;
				uint time_dataoffset = 0;

				// Read transaction with bit timing data
				int count = BeagleApi.bg_i2c_read_bit_timing(handle, ref status, ref time_sop, ref time_duration, ref time_dataoffset, max_bytes, data_in, timing_size, timing);
				string output = output_parse(data_in);				
				string status_string = print_general_status(status);
                buffer.AddRange(data_in); //add to my own buffer

				lines.Add(status_string + " - " + output + " - " + "Iteration: " + i); //added this so see in gui

				if (status_string.Contains("TIMEOUT"))
					continue; //skip if no activity on bus
				// Translate timestamp to ns
				time_sop_ns = TIMESTAMP_TO_NS(time_sop, sample_rate);

				Console.Write("{0:d},{1:d}", i, time_sop_ns);
				Console.Write(",I2C,(");

				if (count < 0) //error condition
					Console.Write("error={0:d},", count);
				else
					Console.Write("Read " + count + " bits. ");

				//print_general_status(status); //below reference funct -> moved up
				Console.Write(status_string);
				print_i2c_status(status); //below reference funct
				Console.Write(")");

				// Check for errors
				if (count <= 0)
				{
					Console.Write("\n");
					Console.Out.Flush(); //what does this do?

					if (count < 0)
						break;

					// If zero data captured, continue
					continue;
				}

				// Print the address and read/write
				Console.Write(",");
				int offset = 0;
				if ((status & BeagleApi.BG_READ_ERR_MIDDLE_OF_PACKET) == 0)
				{
					// Display the start condition
					Console.Write("[S] ");

					if (count >= 1)
					{
						// Determine if byte was NACKed
						int nack = (data_in[0] & BeagleApi.BG_I2C_MONITOR_NACK);

						// Determine if this is a 10-bit address
						if (count == 1 || (data_in[0] & 0xf9) != 0xf0 ||
							(nack != 0))
						{
							// Display 7-bit address
							Console.Write("<{0:x2}:{1:s}>{2:s} ",
								   (data_in[0] & 0xff) >> 1,
								   ((data_in[0] & 0x01) != 0) ? 'r' : 'w',
								   (nack != 0) ? "*" : "");
							offset = 1;
						}
						else
						{
							// Display 10-bit address
							Console.Write("<{0:x3}:{1:s}>{2:s} ", ((data_in[0] << 7) & 0x300) | (data_in[1] & 0xff), ((data_in[0] & 0x01) != 0) ? 'r' : 'w',
								   (nack != 0) ? "*" : "");
							offset = 2;
						}
					}
				}

				// Display rest of transaction - data
				count = count - offset;
				for (int n = 0; n < count; ++n)
				{
					// Determine if byte was NACKed
					int nack = (data_in[offset] & BeagleApi.BG_I2C_MONITOR_NACK);

					Console.Write("{0:x2}{1:s} ",
							data_in[offset] & 0xff, (nack != 0) ? "*" : "");
					++offset;
				}
			}

			// Stop the capture
			//BeagleApi.bg_disable(handle);
			return lines;
		}

		//--- from totalphase documentation
		static ulong TIMESTAMP_TO_NS(ulong stamp, int samplerate_khz)
		{
			return (ulong)(stamp * 1000 / (ulong)(samplerate_khz / 1000));
		}

		string output_parse(ushort[] data)
		{
			string output = "";
			int value = 0;

			if (data[0] == 32)
				output += "Address: ";
			if (data[0] == 33)
				output += "Temperature: ";

			for (int i = 1; i < data.Length; i++)
				output += ((data[i]&0xff).ToString("X") + " "); //bitwise and to filter data

			if (output.Contains("Temp"))
			{
				value = ((data[1] & 0xff) << 8) + ((data[2] & 0xff)); //bitwise and to filter data
				output += "(" + ((value / 4) - 273) + ")";
			}

			return output;
		}

		string print_general_status(uint status)
		{
			string status_string = " ";

			Console.Write(" ");

			// General status codes
			if (status == BeagleApi.BG_READ_OK)
				status_string += ("OK");

			if (status != 0)
			{
				buffer_available = BeagleApi.bg_host_buffer_used(handle);
				status_string += "(" + buffer_available + ":" + BeagleApi.bg_host_buffer_size(handle, 0) + ")";
			}
			if ((status & BeagleApi.BG_READ_TIMEOUT) != 0)
				status_string += ("TIMEOUT ");

			if ((status & BeagleApi.BG_READ_ERR_MIDDLE_OF_PACKET) != 0)
				status_string += ("MIDDLE ");

			if ((status & BeagleApi.BG_READ_ERR_SHORT_BUFFER) != 0)
				status_string += ("SHORT BUFFER ");

			if ((status & BeagleApi.BG_READ_ERR_PARTIAL_LAST_BYTE) != 0)
				status_string += string.Format("PARTIAL_BYTE(bit {0:d}) ", status & 0xff);

			return status_string;
		}


		static void print_i2c_status(uint status)
		{
			// I2C status codes
			if ((status & BeagleApi.BG_READ_I2C_NO_STOP) != 0)
				Console.Write("I2C_NO_STOP ");
		}
	}
}
