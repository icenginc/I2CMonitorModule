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

		public int i2c_write(ushort bytes, byte[] data_out)
		{
			//only for SHT module
			var result = AardvarkApi.aa_i2c_write(handle, 0x44, AardvarkI2cFlags.AA_I2C_NO_FLAGS, bytes, data_out);

			if (result < 0)
				Console.WriteLine("error: {0}\n", AardvarkApi.aa_status_string(result));

			return result;
		}

		public int i2c_read(ushort bytes, out byte[] data_in)
		{
			data_in = Enumerable.Repeat<byte>(0, bytes).ToArray(); //set it to all 0's first

			var result =  AardvarkApi.aa_i2c_read(handle, 0x44, AardvarkI2cFlags.AA_I2C_NO_FLAGS, bytes, data_in);

			if (result < 0)
				Console.WriteLine("error: {0}\n", AardvarkApi.aa_status_string(result));

			return result;
		}

		public ushort return_port()
		{
			return port;
		}

		public uint return_id()
		{
			return id;
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

		ushort port;
		uint id;
		int handle;
		int bitrate = 100;
		int bus_timeout;
		public const int BUS_TIMEOUT = 150;  // ms

		public ushort return_port()
		{
			return port;
		}

		public uint return_id()
		{
			return id;
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

		private void setup_i2c()
		{
			BeagleApi.bg_enable(handle, BeagleProtocol.BG_PROTOCOL_I2C);
		}

		public void snoop_i2c()
		{
			BeagleApi.bg_
		}
	}
}
