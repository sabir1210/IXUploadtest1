using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.Json;
using BDU.ViewModels;
using BDU.UTIL;
using System.Net.Http;
using System.Text;
using System.Net.Http.Headers;

namespace BDU.Services
{
    public class BDUBaseService
    {
        #region "Service Gets"
        protected async Task<ResponseViewModel> Login(string pEmail, string pPwd)
        {
            //HotelViewModel rsModel = new HotelViewModel();
            ResponseViewModel responseViewModel = new ResponseViewModel();        
            try
            {
                login_Filters user = new login_Filters { email = pEmail, password = pPwd };
                HttpClient httpClient = new HttpClient();
                // serialize into json string
                httpClient.BaseAddress = new Uri(GlobalApp.API_URI); 
                httpClient.DefaultRequestHeaders.Accept.Clear();
                // Set header before passing of request
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // Serialize JSON before passing to API
                var jsonContent = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");
                // Call API Method
                var response = await httpClient.PostAsync($"{GlobalApp.API_URI}"+ API.POST_LOGIN, jsonContent);               
                response.EnsureSuccessStatusCode();
                var result = response.Content.ReadAsStringAsync();
                ResponseViewModel rsModel = JsonSerializer.Deserialize<ResponseViewModel>(result.Result);             
                responseViewModel.jsonData = rsModel;
                responseViewModel.status = true;
                responseViewModel.status_code = ((int)UTIL.Enums.ERROR_CODE.SUCCESS).ToString();
            }
            catch (Exception ex)
            {
                responseViewModel.message = ex.Message;
                responseViewModel.status_code = ((int)UTIL.Enums.ERROR_CODE.FAILED).ToString();
                responseViewModel.status = false;
            }          
                  
            return responseViewModel;
           
        }       
        protected async Task<IEnumerable<MappingViewModel>> getCMSEntitiesAndFields()
        {
            List < MappingViewModel > ls= new List<MappingViewModel>();
            //var listByName = await _addressAppService.GetAddressList(name);
            //var mappedByName = _mapper.Map<IEnumerable<AddressViewModel>>(listByName);
            return ls;
        }


        protected async Task<HotelViewModel> getHotelPresets(int pHotelid, string pHotelCode, int pSystemType, int pVersion)
        {
            //var address = await _addressAppService.GetAddressById(addressId);
            //var mappedByName = _mapper.Map<AddressViewModel>(address);
            //  var mapped = _mapper.Map<IEnumerable<PriceViewModel>>(list);
            return new HotelViewModel();
        }
        // pHotelId =0 default
        protected async Task<IEnumerable<HotelViewModel>> getHotelslist(int pHotelId, int status) {
            List<HotelViewModel> ls = new List<HotelViewModel>();
            return ls;
        }
        #endregion
        // Task<HotelViewModel> saveHotelPresets(int pHotelid, string pHotelname, HotelViewModel hotel, int pVersion);
        // Task<HotelViewModel> getHotelPresets(int pHotelid, string pHotelCode, int pSystemType, int pVersion);

        // Task<MappingViewModel> getCMSData(int pHotelId, DateTime pTimeFrom, int status);
        // Task<ResponseViewModel> saveCMSData(int pHotelId, int pUserid, int status, HotelViewModel pHotel);

        // Task<ResponseViewModel> logOut(string pEmail, int pUserid);
        #region "Service Save Data & Log Out"
        protected async Task<ResponseViewModel> saveHotelPresets(int pHotelid, string pHotelname, HotelViewModel hotel, int pVersion)
        {
            //var listByName = await _addressAppService.GetAddressList(name);
            //var mappedByName = _mapper.Map<IEnumerable<AddressViewModel>>(listByName);
            return new ResponseViewModel();
        }
        protected async Task<ResponseViewModel> saveCMSData(int pHotelId, int pUserid, int status, HotelViewModel pHotel) {
           return new ResponseViewModel();
        }
        protected async Task<ResponseViewModel> logOut(string pEmail, int pUserid)
        {
            List<MappingViewModel> ls = new List<MappingViewModel>();
            //var listByName = await _addressAppService.GetAddressList(name);
            //var mappedByName = _mapper.Map<IEnumerable<AddressViewModel>>(listByName);
            return new ResponseViewModel();
        }
      
        #endregion
    }
}
