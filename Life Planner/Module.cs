using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Life_Planner {
	class Module {
		private int moduleID;
		private string moduleName;
		private double moduleCost, moduleDuration;
		private string moduleRequirement;

		public Module() {
			moduleID = -1;
			moduleName = null;
			moduleCost = -1;
			moduleDuration = -1;
			moduleRequirement = null;
		}

		public int getModuleID() {
			return moduleID;
		}
		public void setModuleID(int moduleID) {
			this.moduleID = moduleID;
		}
		public string getModuleName() {
			return moduleName;
		}
		public void setModuleName(string moduleName) {
			this.moduleName = moduleName;
		}
		public double getModuleCost() {
			return moduleCost;
		}
		public void setModuleCost(double moduleCost) {
			this.moduleCost = moduleCost;
		}
		public double getModuleDuration() {
			return moduleDuration;
		}
		public void setModuleDuration(double moduleDuration) {
			this.moduleDuration = moduleDuration;
		}
		public string getModuleRequirement() {
			return moduleRequirement;
		}

		public double calculateLongestTime() {
			return -1;
		}
		public double calculateShortestTime() {
			return -1;
		}
	}
}
