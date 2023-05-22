using System;
using System.Collections.Generic;
using BDU.ViewModels;
using System.Threading.Tasks;

namespace BDU.Interfaces
{
    public interface IBDUService
    {
        #region "GET, DATA RETEIVAL"
        Task<ResponseViewModel> saveHotelPresets(int pHotelid, string pHotelname, HotelViewModel preset, string pVersion);
        Task<HotelViewModel> getHotelPresets(int pHotelid, string pHotelCode, int pSystemType, string pVersion);
        Task<IEnumerable<MappingViewModel>> getCMSEntitiesAndFields();
        Task<EmailConfigurationsViewModel> loadLogMailSettings(string email, string password);
        Task<MappingBindingViewModel> saveEntityField(MappingBindingViewModel model);
        Task<List<ConfigurationAndSettingsViewModel>> getHotelConfigurationAndSettings(ConfigurationAndSettingsViewModel model);
        Task<UserViewModel> getUserDetails();
        Task<List<MappingViewModel>> getCMSData(int pHotelId, DateTime pTimeFrom, int status);
        Task<List<MappingViewModel>> getCMSDataService(int pHotelId, DateTime pTimeFrom);
        Task<List<HotelViewModel>> getHotelslist(int pHotelId, int status);
        Task<List<MappingViewModel>> getFieldMapping();
        Task<ResponseViewModel> savePMSVersion(List<MappingViewModel> mappings, int id = 0, int status = 1, string version = "V1");
        Task<List<PMSVersionsBindingViewModel>> getPMSVersions(int verionId = 0);
         Task<PMSVersionViewModel> getPMSVersion(int verionId);
        Task<List<LOVViewModel>> getLOVData();
        Task<List<PreferenceViewModel>> loadDefaultPreferences();
        #endregion
        #region "SAVE DATA "
        Task<ResponseViewModel> saveAppLog(List<AppLogViewModel> logls, int hotel_id = 0);
        Task<ResponseViewModel> saveCMSData(int hotel_id, List<MappingViewModel> data);
        Task<ResponseViewModel> saveSettingsData( ConfigurationAndSettingsViewModel model, int hotel_id);
        Task<ResponseViewModel> Login(string pEmail, string pPwd);
        Task<ResponseViewModel> logOut();
        #endregion
    }
}
