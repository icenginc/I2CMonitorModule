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
		public Aardvark(ushort port, uint id)
		{
			this.port = port;
			this.id = id;
		}

		ushort port;
		uint id;

		public void i2c_write()
		{
			
		}
	}

	class Beagle
	{
		public Beagle(ushort port, uint id)
		{
			this.port = port;
			this.id = id;
		}

		ushort port;
		uint id;
	}
}
