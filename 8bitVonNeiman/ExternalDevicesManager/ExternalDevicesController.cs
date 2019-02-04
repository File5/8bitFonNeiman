﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _8bitVonNeiman.ExternalDevicesManager.View;
using _8bitVonNeiman.ExternalDevices;
using _8bitVonNeiman.Common;

namespace _8bitVonNeiman.ExternalDevicesManager {
	public class ExternalDevicesController : IExternalDevicesControllerInput, IDeviceManagerFormOutput, IDeviceOutput {

		private DeviceManagerForm _form;

        private readonly DevicesFactory _devicesFactory;
        private ISet<IDeviceInput> _devices = new HashSet<IDeviceInput>();

		public ExternalDevicesController() {
            _devicesFactory = new DevicesFactory(this);
        }

		/// Открывает форму, если она закрыта или закрывает, если открыта
		public void ChangeFormState() {
			if (_form == null) {
				_form = new DeviceManagerForm(this);
				_form.Show();
				_form.ShowAvailableDevices();
			} else {
				_form.Close();
			}
		}

		public void AddExternalDevice() {
            // todo select proper device
            IDeviceInput input = _devicesFactory.GetKeyboard1();
            _devices.Add(input);

            input.OpenForm();
		}

        public void AddDisplay() {
            IDeviceInput input = _devicesFactory.GetDisplay();
            _devices.Add(input);

            input.OpenForm();
        }

		public void FormClosed() {
			_form = null;
		}

        public void DeviceFormClosed(IDeviceInput device) {
            _devices.Remove(device);
        }

        public ExtendedBitArray GetExternalMemory(int address) {
            foreach (var device in _devices) {
                if (device.HasMemory(address)) {
                    return device.GetMemory(address);
                }
            }
            return new ExtendedBitArray();
        }

        public void SetExternalMemory(ExtendedBitArray memory, int address) {
            foreach (var device in _devices) {
                if (device.HasMemory(address)) {
                    device.SetMemory(memory, address);
                    break;
                }
            }
        }
    }
}