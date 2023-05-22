using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BDU.Interfaces;
using BDU.UTIL;
using BDU.ViewModels;
using static BDU.UTIL.Enums;

namespace BDU.Services
{
    public class BDUService : BDUBaseService, IBDUService
    {
        #region "Service Gets"
        JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true, IgnoreNullValues = true, NumberHandling= System.Text.Json.Serialization.JsonNumberHandling.AllowReadingFromString };
        public async Task<ResponseViewModel> Login(string pEmail, string pPwd)
        {
            ResponseViewModel response = new ResponseViewModel();
            try
            {
                response = await base.Login(Convert.ToString(pEmail).Trim(), pPwd);
                if (response.status && response.status_code == ((int)UTIL.Enums.ERROR_CODE.SUCCESS).ToString())
                {
                    ResponseViewModel res = (ResponseViewModel)response.jsonData;
                    //HotelViewModel hotel = (HotelViewModel)res.jsonData;
                    GlobalApp.JWT_Token = res.access_token;
                    HotelViewModel hotel = JsonSerializer.Deserialize<HotelViewModel>(res.jsonData.ToString());                    
                    UTIL.GlobalApp.Hotel_Code = "" + hotel.code;
                    hotel.hotel_code = "" + hotel.code;
                    if (Convert.ToInt16(hotel.isHotelUser) == (int)UTIL.Enums.USERROLES.PMS_Staff)
                    {
                        UTIL.GlobalApp.Hotel_id = hotel.id;
                        UTIL.GlobalApp.PMS_Version_No = hotel.pms_version_id;
                        UTIL.GlobalApp.login_role = USERROLES.PMS_Staff;
                    }
                    else
                    {
                        UTIL.GlobalApp.login_role = USERROLES.Servr_Staff;
                        UTIL.GlobalApp.Hotel_id = 0;
                    }

                    UTIL.GlobalApp.User_id = hotel.id;
                    UTIL.GlobalApp.Hotel_Name = hotel.name;
                    UTIL.GlobalApp.UserName = hotel.email;
                    GlobalApp.Authentication_Done = true;
                }
                else
                {
                    // HotelViewModel hotelsViewModel = (HotelViewModel)responseViewModel.data;
                    GlobalApp.JWT_Token = string.Empty;
                    GlobalApp.Authentication_Done = false;
                }
            }
            catch (Exception ex)
            {
                if (ex.ToString().ToLower().Contains(BDUConstants.API_UNATHENTICATED))
                {
                    UTIL.GlobalApp.JWT_Token = string.Empty;
                    UTIL.GlobalApp.Authentication_Done = false;
                }
                throw new Exception(ex.Message);
            }

            return response;
        }
        public async Task<ResponseViewModel> preferenceLogin(string pEmail, string pPwd)
        {
            ResponseViewModel response = new ResponseViewModel();
            try
            {
                response = await base.Login(Convert.ToString(pEmail).Trim(), pPwd);
                if (response.status && response.status_code == ((int)UTIL.Enums.ERROR_CODE.SUCCESS).ToString())
                {
                    ResponseViewModel res = (ResponseViewModel)response.jsonData;
                    //HotelViewModel hotel = (HotelViewModel)res.jsonData;
                    GlobalApp.JWT_Token = res.access_token;
                    HotelViewModel hotel = JsonSerializer.Deserialize<HotelViewModel>(res.jsonData.ToString());
                    if (Convert.ToInt16(hotel.isHotelUser) == (int)UTIL.Enums.USERROLES.PMS_Staff)
                    {
                        res.status = false;
                        res.message = "For mapping and preference, you need to login with servr special user account, Please try another time or contact support@servrhotels.com!";
                    }
                    else
                        res.status = true;
                    }
            }
            catch (Exception ex)
            {
                if (ex.ToString().ToLower().Contains(BDUConstants.API_UNATHENTICATED))
                {
                    response.status = false;
                    response.message = ex.Message;
                }
                throw new Exception(ex.Message);
            }

            return response;
        }
        public async Task<IEnumerable<MappingViewModel>> getCMSEntitiesAndFields()
        {
            IEnumerable<MappingViewModel> data = null;
            try
            {
                if (string.IsNullOrWhiteSpace(GlobalApp.JWT_Token) || !UTIL.GlobalApp.Authentication_Done) throw new Exception("Authorization Failed");
                //  List<MappingViewModel> ls = new List<MappingViewModel>();
                HttpClient httpClient = new HttpClient();
                // serialize into json string
                httpClient.BaseAddress = new Uri(GlobalApp.API_URI);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                // Set header before passing of request
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + GlobalApp.JWT_Token);
                var response = await httpClient.GetAsync($"{GlobalApp.API_URI}" + API.GET_PMS_ENTITY_FIELD_MAPPINGS);
                response.EnsureSuccessStatusCode();
                var result = response.Content.ReadAsStringAsync();
                data = JsonSerializer.Deserialize<IEnumerable<MappingViewModel>>(result.Result, options);

            }
            catch (Exception ex)
            {
                data = null;
                if (ex.ToString().ToLower().Contains(BDUConstants.API_UNATHENTICATED))
                {
                    UTIL.GlobalApp.JWT_Token = string.Empty;
                    UTIL.GlobalApp.Authentication_Done = false;
                }
                throw ex;
            }
            return data;
        }
        public MappingViewModel TestFieldMapping(PMS_VERSIONS systemType = PMS_VERSIONS.PMS_Desktop)
        {
            if (systemType == PMS_VERSIONS.PMS_Web)
            {
                MappingViewModel mappingViewModel = new MappingViewModel { id = 2, entity_name = "Check In", status = 3, saves_status = 0, entity_type = (int)UTIL.Enums.ENTITY_TYPES.SYNC, mode = 2, pmsformid = "https://app.littlehotelier.com/extranet/properties/16631/reservations/21361984/edit", xpath = "" };
                List<EntityFieldViewModel> frmls = new List<EntityFieldViewModel>();
                var form1 = new FormViewModel { id = 1, pmspagename = "form1", pmspageid = "https://app.littlehotelier.com/extranet/properties/16631/reservations/21361984/edit" };
                frmls.Add(new EntityFieldViewModel { sr = 0, fuid = "4abet8d0-ce8a-11eb-b5e9-ecb1d75edbb3", pms_field_name = "", field_desc = "", pms_field_xpath = "/html[1]/frameset[1]/frame[1]", default_value = "", data_type = 2, value = "", control_type = 19, is_reference = 0, is_unmapped = 1, mandatory = 1 });
                frmls.Add(new EntityFieldViewModel { sr = 0, fuid = "4abea0d0-ce8a-11eb-b5e9-ecb1d75edbb3", pms_field_name = "", field_desc = "", pms_field_xpath = "/html[1]/frameset[1]/frame[2]", default_value = "", data_type = 2, value = "", control_type = 19, is_reference = 0, is_unmapped = 1, mandatory = 1 });
                frmls.Add(new EntityFieldViewModel { sr = 1, fuid = "4abea140-ce8a-11eb-b5e9-ecb1d75edbb3", pms_field_name = "details", field_desc = "Booking Ref", parent_field_id = "4abet8d0-ce8a-11eb-b5e9-ecb1d75edbb3", pms_field_expression = "//input[@id='details']", pms_field_xpath = "", is_reference = 1, is_unmapped = 3, default_value = "", data_type = 2, value = "", mandatory = 1 });
                frmls.Add(new EntityFieldViewModel { sr = 2, fuid = "4abea1aa-ce8a-11eb-b5e9-ecb1d75edbb3", pms_field_name = "hotel_id", field_desc = "Hotel", parent_field_id = "4abet8d0-ce8a-11eb-b5e9-ecb1d75edbb3", pms_field_expression = "//select[@id='hotel_id']", pms_field_xpath = "", is_reference = 1, is_unmapped = 3, default_value = "0", data_type = 1, value = "", mandatory = 1 });
                frmls.Add(new EntityFieldViewModel { sr = 3, fuid = "4abea20e-ce8a-11eb-b5e9-ecb1d75edbb3", pms_field_name = "check_in_date_display", field_desc = "Arrival Date", parent_field_id = "4abet8d0-ce8a-11eb-b5e9-ecb1d75edbb3", pms_field_xpath = "//input[@id='check_in_date_display']", is_reference = 1, is_unmapped = 3, default_value = "", data_type = 3, value = "", mandatory = 1 });
                frmls.Add(new EntityFieldViewModel { sr = 4, fuid = "4abea26e-ce8a-11eb-b5e9-ecb1d75edbb3", pms_field_name = "check_out_date_display", field_desc = "departure Date", parent_field_id = "4abet8d0-ce8a-11eb-b5e9-ecb1d75edbb3", pms_field_xpath = "//input[@id='check_out_date_display']", is_reference = 1, is_unmapped = 3, default_value = "", data_type = 3, value = "", mandatory = 1 });
                frmls.Add(new EntityFieldViewModel { sr = 5, fuid = "4abea2d1-ce8a-11eb-b5e9-ecb1d75edbb3", pms_field_name = "phone_number", field_desc = "Phone", pms_field_xpath = "//input[@id='phone_number']", parent_field_id = "4abet8d0-ce8a-11eb-b5e9-ecb1d75edbb3", default_value = "", data_type = 2, value = "", is_reference = 0, is_unmapped = 0, mandatory = 1 });
                frmls.Add(new EntityFieldViewModel { sr = 6, fuid = "4abea32e-ce8a-11eb-b5e9-ecb1d75edbb3", pms_field_name = "rate_plan_Id", field_desc = "rate", parent_field_id = "4abet8d0-ce8a-11eb-b5e9-ecb1d75edbb3", pms_field_xpath = "//select[@id='rate_plan_Id']", default_value = "", data_type = 1, value = "", is_reference = 1, is_unmapped = 0, mandatory = 1 });
                frmls.Add(new EntityFieldViewModel { sr = 7, fuid = "4abea38e-ce8a-11eb-b5e9-ecb1d75edbb3", pms_field_name = "room_id", field_desc = "Room Number", parent_field_id = "4abet8d0-ce8a-11eb-b5e9-ecb1d75edbb3", pms_field_xpath = "//select[@id='room_id']", default_value = "100", data_type = 1, value = "", is_reference = 1, is_unmapped = 0, mandatory = 1 });
                frmls.Add(new EntityFieldViewModel { sr = 8, fuid = "4abea3eb-ce8a-11eb-b5e9-ecb1d75edbb3", pms_field_name = "number_adults", field_desc = "Number Adult ", parent_field_id = "4abet8d0-ce8a-11eb-b5e9-ecb1d75edbb3", pms_field_xpath = "//input[@id='number_adults']", default_value = "0", data_type = 2, value = "", is_reference = 1, is_unmapped = 0, mandatory = 1 });
                frmls.Add(new EntityFieldViewModel { sr = 9, fuid = "4abea44c-ce8a-11eb-b5e9-ecb1d75edbb3", pms_field_name = "guest_first_name", field_desc = "First Name", parent_field_id = "4abet8d0-ce8a-11eb-b5e9-ecb1d75edbb3", pms_field_xpath = "", default_value = "0", data_type = 2, status = 0, value = "", is_reference = 1, is_unmapped = 0, mandatory = 0 });
                frmls.Add(new EntityFieldViewModel { sr = 10, fuid = "4abea4ac-ce8a-11eb-b5e9-ecb1d75edbb3", pms_field_name = "guest_last_name", field_desc = "Last Name", parent_field_id = "4abet8d0-ce8a-11eb-b5e9-ecb1d75edbb3", pms_field_xpath = "", default_value = "0", data_type = 2, status = 0, value = "", is_reference = 1, is_unmapped = 0, mandatory = 1 });
                frmls.Add(new EntityFieldViewModel { sr = 11, fuid = "4abea50c-ce8a-11eb-b5e9-ecb1d75edbb3", pms_field_name = "guest_email", field_desc = "Email", parent_field_id = "4abet8d0-ce8a-11eb-b5e9-ecb1d75edbb3", pms_field_xpath = "//input[@id='guest_email']", default_value = "", data_type = 2, value = "", is_reference = 1, is_unmapped = 0, mandatory = 1 });
                frmls.Add(new EntityFieldViewModel { sr = 12, fuid = "4abea569-ce8a-11eb-b5e9-ecb1d75edbb3", pms_field_name = "guest_organisation", parent_field_id = "4abet8d0-ce8a-11eb-b5e9-ecb1d75edbb3", field_desc = "Address", pms_field_xpath = "//input[@id='guest_organisation']", default_value = "", data_type = 2, value = "", is_reference = 1, is_unmapped = 0, mandatory = 1 });
                frmls.Add(new EntityFieldViewModel { sr = 13, fuid = "4abea5c6-ce8a-11eb-b5e9-ecb1d75edbb3", pms_field_name = "guest_country", parent_field_id = "4abet8d0-ce8a-11eb-b5e9-ecb1d75edbb3", field_desc = "Country", pms_field_xpath = "//select[@id='guest_country']", default_value = "", data_type = 2, value = "", is_reference = 1, is_unmapped = 0, mandatory = 0 });
                frmls.Add(new EntityFieldViewModel { sr = 14, fuid = "4abea62a-ce8a-11eb-b5e9-ecb1d75edbb3", pms_field_name = "payment_card_name", parent_field_id = "4abea0d0-ce8a-11eb-b5e9-ecb1d75edbb3", field_desc = "Card Name", pms_field_xpath = "//input[@id='payment_card_name']", default_value = "", data_type = 2, value = "", is_reference = 1, is_unmapped = 0, mandatory = 0 });
                frmls.Add(new EntityFieldViewModel { sr = 15, fuid = "4abea68a-ce8a-11eb-b5e9-ecb1d75edbb3", pms_field_name = "payment_card_expiry_month", parent_field_id = "4abet8d0-ce8a-11eb-b5e9-ecb1d75edbb3", field_desc = "Expiry Month", pms_field_xpath = "//input[@id='payment_card_expiry_month']", default_value = "", data_type = 3, value = "", is_reference = 1, is_unmapped = 0, mandatory = 0 });
                frmls.Add(new EntityFieldViewModel { sr = 16, fuid = "4abea6e7-ce8a-11eb-b5e9-ecb1d75edbb3", pms_field_name = "payment_card_expiry_year", parent_field_id = "4abet8d0-ce8a-11eb-b5e9-ecb1d75edbb3", field_desc = "Expiry Year", pms_field_xpath = "//input[@id='payment_card_expiry_year']", default_value = "", data_type = 3, is_reference = 1, is_unmapped = 0, value = "", mandatory = 0 });
                frmls.Add(new EntityFieldViewModel { sr = 17, fuid = "4abea747-ce8a-11eb-b5e9-ecb1d75edbb3", pms_field_name = "guest_id_document_type", parent_field_id = "4abet8d0-ce8a-11eb-b5e9-ecb1d75edbb3", field_desc = "Documents Type", pms_field_xpath = "", default_value = "", data_type = 4, is_reference = 1, is_unmapped = 0, value = "", status = 0, mandatory = 1 });
                frmls.Add(new EntityFieldViewModel { sr = 18, fuid = Guid.NewGuid().ToString(), pms_field_name = "guest_id_number", parent_field_id = "4abea0d0-ce8a-11eb-b5e9-ecb1d75edbb3", field_desc = "Id Number", pms_field_xpath = "//input[@id='guest_id_number']", default_value = "", data_type = 2, is_reference = 1, is_unmapped = 0, value = "", mandatory = 1 });
                frmls.Add(new EntityFieldViewModel { sr = 19, fuid = Guid.NewGuid().ToString(), pms_field_name = "guest_comments", parent_field_id = "4abet8d0-ce8a-11eb-b5e9-ecb1d75edbb3", field_desc = "Comments", pms_field_xpath = "//input[@id='guest_comments']", default_value = "", data_type = 2, is_reference = 1, is_unmapped = 0, value = "", mandatory = 0 });
                frmls.Add(new EntityFieldViewModel { sr = 20, fuid = Guid.NewGuid().ToString(), pms_field_name = "btnSubmit", parent_field_id = "4abea0d0-ce8a-11eb-b5e9-ecb1d75edbb3", field_desc = "Submit", pms_field_xpath = "//a[contains(text(),'Submit')]", default_value = "", data_type = 0, control_type = 17, is_reference = 0, is_unmapped = 0, value = "Submit", mandatory = 0 });
                form1.fields = frmls;

                //form2.fields.AddRange(fieldForm2.ToArray());
                mappingViewModel.forms = new List<FormViewModel>();
                mappingViewModel.forms.Add(form1);
                return mappingViewModel;
            }
            else
            {
                MappingViewModel mappingViewModel = new MappingViewModel { id = 2, entity_name = "Check In", status = 1, saves_status = 0, entity_type = (int)UTIL.Enums.ENTITY_TYPES.SYNC, mode = 2, pmsformid = "form", xpath = "/Pane[@ClassName\u0022#32769\u0022][@Name=\u0022Desktop 1\u0022]/ Window[@Name=\u0022PMS Form\u0022][@AutomationId=\u0022PMSTabForm\u0022]" };
                //MappingViewModel mappingViewModel = new MappingViewModel();
                List<EntityFieldViewModel> fieldForm1 = new List<EntityFieldViewModel>();// Hotel_id - 4abea0d0-ce8a-11eb-b5e9-ecb1d75edbb3, 4abea140-ce8a-11eb-b5e9-ecb1d75edbb3
                var form1 = new FormViewModel { id = 1, pmspagename = "Info" };
                fieldForm1.Add(new EntityFieldViewModel { sr = 1, fuid = "4abea0d0-ce8a-11eb-b5e9-ecb1d75edbb3", id = 1, pms_field_expression = "reference", pms_field_name = "reference", field_desc = "Booking Ref", pms_field_xpath = "/Window[@AutomationId=\reference", default_value = "", is_reference = 1, is_unmapped = 0, data_type = 2, status = 1, value = "", mandatory = 1 });
                //fields.Add(new EntityFieldViewModel { sr = 2, id = 2,  pms_field_name = "hotel_name", sort_order = 2, field_desc = "Hotel", name = "", default_value = "", data_type = DATA_TYPES.INT, value = "", schema_field_name = "hotel_name", mandatory = 1 });
                fieldForm1.Add(new EntityFieldViewModel { sr = 2, fuid = "4abea140-ce8a-11eb-b5e9-ecb1d75edbb3", id = 2, pms_field_expression = "hotel_id", pms_field_name = "hotel_id", field_desc = "Hotel", pms_field_xpath = "", default_value = "Servr", is_reference = 0, is_unmapped = 0, data_type = 1, value = "", mandatory = 1 });
                fieldForm1.Add(new EntityFieldViewModel { sr = 2, fuid = "4abe675-ce8a-11eb-b5e9-ecb1d75edbb3", id = 2, pms_field_expression = "gbox", pms_field_name = "gbox", field_desc = "Grid", pms_field_xpath = "", default_value = "Maria", is_reference = 0, is_unmapped = 0, data_type = 1, status = 1, control_type = 26, action_type = 17, value = "", mandatory = 1 });
                fieldForm1.Add(new EntityFieldViewModel { sr = 3, fuid = "4abea1aa-ce8a-11eb-b5e9-ecb1d75edbb3", id = 3, pms_field_name = "arrival_date", field_desc = "Arrival Date", pms_field_xpath = "", is_reference = 0, is_unmapped = 0, default_value = "", data_type = 4, value = "", mandatory = 1 });
                //fields.Add(new EntityFieldViewModel { sr = 4, id = 4,  pms_field_name = "birth_date", sort_order = 4, field_desc = "Birth Date", name = "", default_value = "", data_type = DATA_TYPES.DATETIME, value = "", schema_field_name = "departure_date", mandatory = 1 });
                fieldForm1.Add(new EntityFieldViewModel { sr = 4, fuid = "4abea20e-ce8a-11eb-b5e9-ecb1d75edbb3", id = 4, pms_field_name = "departure_date", field_desc = "departure_date Date", pms_field_xpath = "departure_date", is_reference = 0, is_unmapped = 0, default_value = "", data_type = 4, value = "", mandatory = 1, status = 1 });
                fieldForm1.Add(new EntityFieldViewModel { sr = 8, fuid = "4abea2d1-ce8a-11eb-b5e9-ecb1d75edbb3", id = 8, pms_field_name = "phone_number", field_desc = "Phone", pms_field_xpath = "phone_number", default_value = "+923454069753", is_reference = 0, is_unmapped = 0, action_type = 9, data_type = 2, value = "+923454069753", mandatory = 0, status = 1 });
                fieldForm1.Add(new EntityFieldViewModel { sr = 9, fuid = "4abea32e-ce8a-11eb-b5e9-ecb1d75edbb3", id = 9, pms_field_name = "room_number", field_desc = "Room Number", pms_field_xpath = "room_number", default_value = "1001", is_reference = 0, action_type = 9, is_unmapped = 0, data_type = 2, value = "1001", mandatory = 1, status = 1 });
                fieldForm1.Add(new EntityFieldViewModel { sr = 10, fuid = "4abea32e-ce8a-11eb-b5e9-ecb1d75edbb3", id = 10, pms_field_name = "email", field_desc = "Email", pms_field_xpath = "email", default_value = "zahid.nawaz@gmail.com", is_reference = 0, action_type = 9, is_unmapped = 0, data_type = 2, value = "", mandatory = 0 });
                fieldForm1.Add(new EntityFieldViewModel { sr = 12, fuid = "4abea38e-ce8a-11eb-b5e9-ecb1d75edbb3", id = 12, pms_field_name = "status", field_desc = "status", pms_field_xpath = "status", default_value = "Checked In", is_reference = 0, is_unmapped = 0, data_type = 2, value = "", mandatory = 1, status = 1 });
                fieldForm1.Add(new EntityFieldViewModel { sr = 13, fuid = "4abea3eb-ce8a-11eb-b5e9-ecb1d75edbb3", id = 13, pms_field_expression = "UIControl1023", pms_field_name = "created_At", field_desc = "created_At", pms_field_xpath = "created_At", is_reference = 0, is_unmapped = 0, default_value = "", data_type = 4, value = "", mandatory = 1 });
                fieldForm1.Add(new EntityFieldViewModel { sr = 14, fuid = "4abea44c-ce8a-11eb-b5e9-ecb1d75edbb3", id = 14, pms_field_name = "updated_At", field_desc = "updated_At", pms_field_xpath = "", default_value = "", is_reference = 0, is_unmapped = 0, data_type = 4, value = "", mandatory = 0 });
                fieldForm1.Add(new EntityFieldViewModel { sr = 15, fuid = "4abea4ac-ce8a-11eb-b5e9-ecb1d75edbb3", id = 15, pms_field_name = "btnSubmit", field_desc = "Submit Form", pms_field_xpath = "btnSubmit", default_value = "", data_type = 0, is_reference = 0, is_unmapped = 0, control_type = 23, action_type = 9, value = "Submit", status = 1, mandatory = 1 });
                form1.fields = fieldForm1;
                var form2 = new FormViewModel { id = 2, pmspagename = "Credit Card" };
                List<EntityFieldViewModel> fieldForm2 = new List<EntityFieldViewModel>();
                fieldForm2.Add(new EntityFieldViewModel { sr = 16, fuid = "4abea569-ce8a-11eb-b5e9-ecb1d75edbb3", id = 16, pms_field_expression = "pic_box", pms_field_name = "pic_box", field_desc = "picture", pms_field_xpath = "", default_value = "", is_reference = 0, is_unmapped = 0, data_type = 2, control_type = 18, value = "", status = 1, mandatory = 0 });
                fieldForm2.Add(new EntityFieldViewModel { sr = 17, fuid = "4abea5c6-ce8a-11eb-b5e9-ecb1d75edbb3", id = 17, pms_field_expression = "card_number", pms_field_name = "card_number", field_desc = "card number", pms_field_xpath = "", is_reference = 0, is_unmapped = 0, default_value = "", data_type = 2, value = "", status = 1, mandatory = 1 });
                fieldForm2.Add(new EntityFieldViewModel { sr = 18, fuid = "4abea62a-ce8a-11eb-b5e9-ecb1d75edbb3", id = 18, pms_field_expression = "UIcardholder_name", pms_field_name = "cardholder_name", field_desc = "Card holder Name Date", is_reference = 0, is_unmapped = 0, status = 1, action_type = 9, pms_field_xpath = "", default_value = "", data_type = 2, value = "", mandatory = 1 });
                fieldForm2.Add(new EntityFieldViewModel { sr = 19, fuid = "4abea68a-ce8a-11eb-b5e9-ecb1d75edbb3", id = 19, pms_field_expression = "UIC10120", pms_field_name = "card_expiry_date", field_desc = "card expiry", pms_field_xpath = "", is_reference = 0, is_unmapped = 0, default_value = "", data_type = 3, value = "", mandatory = 1, status = 1, });
                fieldForm2.Add(new EntityFieldViewModel { sr = 20, fuid = "4abea68a-ce8a-11eb-b5e9-ecb1d75edbb3", id = 20, pms_field_expression = "UIC10120", pms_field_name = "card_address", field_desc = "card address", pms_field_xpath = "", is_reference = 0, is_unmapped = 0, action_type = 9, default_value = "", data_type = 2, value = "", status = 1, mandatory = 1 });
                form2.fields = fieldForm2;
                //form2.fields.AddRange(fieldForm2.ToArray());
                mappingViewModel.forms = new List<FormViewModel>();
                mappingViewModel.forms.Add(form1);
                mappingViewModel.forms.Add(form2);
                return mappingViewModel;

            }
            //return null;
        }
        public async Task<HotelViewModel> LoadDefaultPresets(int hotel_id, string version)
        {

            //  PresetViewModel preset = new PresetViewModel{ id=0, hotel_id= hotel_id, version= version, hotel_code=GlobalApp.Hotel_Code, Status=0, time=GlobalApp.CurrentDateTime, created_by=GlobalApp.User_id,  updated_by = GlobalApp.User_id };
            HotelViewModel hotel = new HotelViewModel { pms_application_path_withname = "", name = "", hotel_code = "", id = hotel_id, system_type = ((int)UTIL.Enums.PMS_VERSIONS.PMS_Desktop).ToString() };
            // Entity Defaults
            List<BasicEntityFieldViewModel> mFields = null;
            List<MappingViewModel> data = new List<MappingViewModel>();
            try
            {
                if (string.IsNullOrWhiteSpace(GlobalApp.JWT_Token) || !UTIL.GlobalApp.Authentication_Done) throw new Exception("Authorization Failed");
                //  List<MappingViewModel> ls = new List<MappingViewModel>();
                HttpClient httpClient = new HttpClient();
                // serialize into json string
                httpClient.BaseAddress = new Uri(GlobalApp.API_URI);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                // Set header before passing of request
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + GlobalApp.JWT_Token);
                var response = await httpClient.GetAsync($"{GlobalApp.API_URI}" + API.GET_PMS_ENTITY_FIELD_MAPPINGS);
                response.EnsureSuccessStatusCode();

                var result = response.Content.ReadAsStringAsync();
                ResponseViewModel res = JsonSerializer.Deserialize<ResponseViewModel>(result.Result, options);

                if (res.status_code.ToString() == ((int)UTIL.Enums.ERROR_CODE.SUCCESS).ToString())
                    mFields = JsonSerializer.Deserialize<List<BasicEntityFieldViewModel>>(res.jsonData.ToString(), options);
                if (mFields != null)
                {

                    List<MappingViewModel> EntityMappings = new List<MappingViewModel>();
                    var entities = (from ents in mFields
                                    where ents.status == (int)UTIL.Enums.STATUSES.Active
                                    select new { entity_id = ents.entity_id, entity_type = ents.entity_type, status = ents.status }
                                          ).Distinct();

                    //******************************  Entity Type UTIL.Enums.ENTITIES.CHECKIN *******************************//
                    foreach (var ets in entities)
                    {

                        List<FormViewModel> formsls = new List<FormViewModel>();

                        FormViewModel frm = new FormViewModel { id = 1, pmspageid = "form1" };

                        var fields = (from flds in mFields
                                      where flds.status == (int)UTIL.Enums.STATUSES.Active && flds.entity_id == ets.entity_id
                                      orderby flds.sr
                                      select flds);
                        List<EntityFieldViewModel> fieldsls = new List<EntityFieldViewModel>();
                        //  
                        foreach (BasicEntityFieldViewModel fds in fields)
                        {
                            fieldsls.Add(new EntityFieldViewModel { fuid = fds.uuid, field_desc = fds.field_desc, sr = fds.sr, status = fds.status, pms_field_expression = fds.pms_field_expression, pms_field_xpath = fds.pms_field_xpath, default_value = fds.default_value, is_unmapped = fds.is_unmapped, is_reference = fds.is_reference, format = fds.format, pms_field_name = fds.pms_field_name });
                        }
                        frm.fields = fieldsls;
                        formsls.Add(frm);
                        UTIL.Enums.ENTITIES entity = (UTIL.Enums.ENTITIES)ets.entity_id;
                        EntityMappings.Add(new MappingViewModel { id = ets.entity_id, entity_Id = ets.entity_id, entity_type = UTIL.GlobalApp.Get_Entity_Type(ets.entity_id), mode = 1, status = ets.status, entity_name = entity.ToString(), forms = formsls, data = null });
                        ////******************************  Entity Type UTIL.Enums.ENTITIES.Billing Details *******************************//

                    }
                    data.AddRange(EntityMappings);
                    hotel.mappings = data;
                }//outer data


                if (hotel != null)
                {
                    //Set All Entities to Hotel
                    //if(hotel.mappings.Where(x=>x.entity_Id== (int)UTIL.Enums.ENTITIES.ACCESS).FirstOrDefault()== null) { 
                    MappingViewModel accessEntity = data.Where(x => x.entity_type == (int)UTIL.Enums.ENTITY_TYPES.ACCESS_MNGT).FirstOrDefault();
                    if (accessEntity == null)
                    {
                        accessEntity = new MappingViewModel { id = (int)UTIL.Enums.ENTITIES.ACCESS, entity_Id = (int)UTIL.Enums.ENTITIES.ACCESS, entity_name = UTIL.Enums.ENTITIES.ACCESS.ToString(), status = 1, entity_type = (int)UTIL.Enums.ENTITY_TYPES.ACCESS_MNGT };
                        List<EntityFieldViewModel> accessflds = new List<EntityFieldViewModel>();
                        FormViewModel form = new FormViewModel { id = 1, pmspagetitle = "Access Credentials", pmspageid = "login" };
                        accessflds.Add(new EntityFieldViewModel { sr = 1, fuid = (Guid.NewGuid()).ToString(), id = 1, pms_field_name = "", field_desc = "Application Name", pms_field_xpath = "", is_reference = 1, default_value = "", is_unmapped = 0, data_type = 2, value = "BT78089", mandatory = 1 });
                        accessflds.Add(new EntityFieldViewModel { sr = 2, fuid = (Guid.NewGuid()).ToString(), id = 2, pms_field_name = "", field_desc = "property id", pms_field_xpath = "", default_value = "3194", data_type = 1, is_unmapped = 0, value = "3194", mandatory = 0 });
                        accessflds.Add(new EntityFieldViewModel { sr = 3, fuid = (Guid.NewGuid()).ToString(), id = 2, pms_field_name = "", field_desc = "login name", pms_field_xpath = "", default_value = "blazor", data_type = 1, is_unmapped = 0, value = "blazor", mandatory = 0 });
                        accessflds.Add(new EntityFieldViewModel { sr = 4, fuid = (Guid.NewGuid()).ToString(), id = 3, pms_field_name = "", field_desc = "password", pms_field_xpath = "", default_value = "Blazor@018", data_type = 1, is_unmapped = 0, value = "Blazor@018", mandatory = 0 });

                        form.fields = accessflds;
                        List<FormViewModel> frmls = new List<FormViewModel>();
                        frmls.Add(form);
                        accessEntity.forms = frmls;
                        data.Add(accessEntity);
                        hotel.mappings = data;
                    }

                    //}// if(hotel.mappings.Where(x=>x.entity_Id== (int)UTIL.Enums.ENTITIES.ACCESS).FirstOrDefault()== null)

                    //  Load Default Preferences
                    // PreferenceViewModel mappingViewModel = new MappingViewModel { id = 0, entity_name = "Check In", status = 1, saves_status = 0, entity_type = (int)UTIL.Enums.ENTITY_TYPES.SYNC, mode = 2, pmsformid = "form", xpath = "/Pane[@ClassName\u0022#32769\u0022][@Name=\u0022Desktop 1\u0022]/ Window[@Name=\u0022PMS Form\u0022][@AutomationId=\u0022PMSTabForm\u0022]" };
                    List<PreferenceViewModel> preferences = new List<PreferenceViewModel>();

                    preferences.Add(new PreferenceViewModel { key = "1", id = 1, Status = (int)UTIL.Enums.STATUSES.Active, color = "#FFFFFF", value = "0" });
                    preferences.Add(new PreferenceViewModel { key = "2", id = 2, Status = (int)UTIL.Enums.STATUSES.Active, color = "#FFFFFF", value = "1" });
                    preferences.Add(new PreferenceViewModel { key = "3", id = 3, Status = (int)UTIL.Enums.STATUSES.Active, color = "#FFFFFF", value = "10" });
                    preferences.Add(new PreferenceViewModel { key = "4", id = 4, Status = (int)UTIL.Enums.STATUSES.Active, color = "#FFFFFF", value = "25" });
                    preferences.Add(new PreferenceViewModel { key = "5", id = 5, Status = (int)UTIL.Enums.STATUSES.Active, color = "#FFFFFF", value = "30" });
                    hotel.preferences = preferences;

                }




            }
            catch (Exception ex)
            {
                if (ex.ToString().ToLower().Contains(BDUConstants.API_UNATHENTICATED))
                {
                    UTIL.GlobalApp.JWT_Token = string.Empty;
                    UTIL.GlobalApp.Authentication_Done = false;
                }
                throw ex;
            }


            return hotel;
        }
        public async Task<List<MappingViewModel>> getFieldMapping()
        {
            ResponseViewModel resp = new ResponseViewModel();
            List<MappingViewModel> ls = new List<MappingViewModel>();// new MappingViewModel();// { id = 0, entity_name = "Check In", status = 1, saves_status = 0, entity_type = (int)UTIL.Enums.ENTITY_TYPES.SYNC, mode = 2, pmsformid = "form", forms=new for, xpath = "/Pane[@Name=\u0022Desktop 1\u0022][@ClassName=\u0022#32769\u0022]/Window[@AutomationId=\u0022PMSForm\u0022][@Name=\u0022PMS Form\u0022]" };
            EnumViewModel enm = new EnumViewModel { id = 0 };
            try
            {

                HttpClient httpClient = new HttpClient();
                // serialize into json string
                httpClient.BaseAddress = new Uri(GlobalApp.API_URI);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                // Set header before passing of request
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await httpClient.GetAsync(GlobalApp.API_URI + API.GET_PMS_ENTITY_FIELD_MAPPINGS);
                response.EnsureSuccessStatusCode();
                var result = response.Content.ReadAsStringAsync();
                MappingViewModel mapping = JsonSerializer.Deserialize<MappingViewModel>(result.Result, options);
                resp.jsonData = mapping;
                resp.status = true;
                resp.status_code = ((int)UTIL.Enums.ERROR_CODE.SUCCESS).ToString();

                if (resp.status_code.ToString() == ((int)UTIL.Enums.ERROR_CODE.SUCCESS).ToString())
                    ls = JsonSerializer.Deserialize<List<MappingViewModel>>(resp.jsonData.ToString(), options);
                else
                    throw new Exception("Failed to get hotels data from CMS.");
            }
            catch (Exception ex)
            {
                if (ex.ToString().ToLower().Contains(BDUConstants.API_UNATHENTICATED))
                {
                    UTIL.GlobalApp.JWT_Token = string.Empty;
                    UTIL.GlobalApp.Authentication_Done = false;
                }
                throw ex;
            }

            return ls;
        }
        public async Task<EmailConfigurationsViewModel> loadLogMailSettings(string email, string password)
        {
            EmailConfigurationsViewModel emailConfig = new EmailConfigurationsViewModel();          
            try
            {
                //***********************Filters************************************//
                login_Filters filters = new login_Filters { email = BDUConstants.API_SPECIAL_AUTHENTICATION_USER, password= BDUConstants.API_SPECIAL_AUTHENTICATION_PWD };

                var httpClient = new HttpClient();

                httpClient.BaseAddress = new Uri(GlobalApp.API_URI);             
                var inputContent = new StringContent(JsonSerializer.Serialize(filters), Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(GlobalApp.API_URI + API.GET_LOG_CONFIGURATIONS_SETTINGS, inputContent);
                response.EnsureSuccessStatusCode();
                var result = response.Content.ReadAsStringAsync();
                ServiceConfigurationResViewModel resp = JsonSerializer.Deserialize<ServiceConfigurationResViewModel>(result.Result, options);
                if (resp != null && !string.IsNullOrWhiteSpace(Convert.ToString(resp.jsonData)))
                {
                    emailConfig = JsonSerializer.Deserialize<EmailConfigurationsViewModel>(resp.jsonData.ToString(), options); //JsonSerializer.Deserialize<List<PMSVersionViewModel>>(resp.jsonData.ToString(), options);
                    if (!string.IsNullOrWhiteSpace("" + resp.utctime))
                    {
                       // emailConfig.utctime = Convert.ToDateTime(resp.utctime);
                        GlobalApp.DifferenceinSecs = ( Convert.ToDateTime(resp.utctime)- System.DateTime.UtcNow).TotalSeconds+1;
                    }
                    else
                        GlobalApp.DifferenceinSecs = 0.0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return emailConfig;
        }
        public async Task<List<ConfigurationAndSettingsViewModel>> getHotelConfigurationAndSettings(ConfigurationAndSettingsViewModel model)
        {
           // ResponseViewModel resp = new ResponseViewModel();
            List<ConfigurationAndSettingsViewModel> ls = new List<ConfigurationAndSettingsViewModel>();// new MappingViewModel();// { id = 0, entity_name = "Check In", status = 1, saves_status = 0, entity_type = (int)UTIL.Enums.ENTITY_TYPES.SYNC, mode = 2, pmsformid = "form", forms=new for, xpath = "/Pane[@Name=\u0022Desktop 1\u0022][@ClassName=\u0022#32769\u0022]/Window[@AutomationId=\u0022PMSForm\u0022][@Name=\u0022PMS Form\u0022]" };
            if (string.IsNullOrWhiteSpace(GlobalApp.JWT_Token) || !UTIL.GlobalApp.Authentication_Done) throw new Exception("Authorization Failed");

            //***********************Filters************************************//
            pms_lov_filters filters = new pms_lov_filters { table = "bdu_status_mappings", select = "pms_version_id,status,target_status" };
            // EnumViewModel enm = new EnumViewModel { id = 0 };
            try
            {

                HttpClient httpClient = new HttpClient();
                // serialize into json string
                httpClient.BaseAddress = new Uri(GlobalApp.API_URI);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                // Set header before passing of request
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var inputContent = new StringContent(JsonSerializer.Serialize(filters), Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(GlobalApp.API_URI + API.POST_GET_LOV_DATA, inputContent);
                response.EnsureSuccessStatusCode();
                var result = response.Content.ReadAsStringAsync();
                ServiceResponseViewModel resp = JsonSerializer.Deserialize<ServiceResponseViewModel>(result.Result, options);
                if (resp != null && !string.IsNullOrWhiteSpace(Convert.ToString(resp.jsonData)))
                    ls = JsonSerializer.Deserialize<List<ConfigurationAndSettingsViewModel>>(Convert.ToString(resp.jsonData), options);
                resp.status = 1;
                resp.status_code = ((int)UTIL.Enums.ERROR_CODE.SUCCESS).ToString();

                //if (resp.status_code.ToString() == ((int)UTIL.Enums.ERROR_CODE.SUCCESS).ToString())
                //    ls = JsonSerializer.Deserialize<List<ConfigurationAndSettingsViewModel>>(resp.jsonData.ToString(), options);
                //else
                //    throw new Exception("Failed to get hotels data from CMS.");
            }
            catch (Exception ex)
            {
                if (ex.ToString().ToLower().Contains(BDUConstants.API_UNATHENTICATED))
                {
                    UTIL.GlobalApp.JWT_Token = string.Empty;
                    UTIL.GlobalApp.Authentication_Done = false;
                }
                throw ex;
            }

            return ls;
        }
        public async Task<List<PreferenceViewModel>> loadDefaultPreferences()
        {
            List<PreferenceViewModel> preferences = new List<PreferenceViewModel>();

            preferences.Add(new PreferenceViewModel { key = "1", Status = (int)UTIL.Enums.STATUSES.Active, color = "#FFFFFF", value = "0" });
            preferences.Add(new PreferenceViewModel { key = "2", Status = (int)UTIL.Enums.STATUSES.Active, color = "#FFFFFF", value = "1" });
            preferences.Add(new PreferenceViewModel { key = "3", Status = (int)UTIL.Enums.STATUSES.Active, color = "#FFFFFF", value = "10" });
            preferences.Add(new PreferenceViewModel { key = "4", Status = (int)UTIL.Enums.STATUSES.Active, color = "#FFFFFF", value = "25" });
            preferences.Add(new PreferenceViewModel { key = "5", Status = (int)UTIL.Enums.STATUSES.Active, color = "#FFFFFF", value = "30" });
            return preferences;
        }
        public async Task<List<MappingViewModel>> getCMSData(int pHotelId, DateTime pTimeFrom, int entityId = 0)
        {
            List<MappingViewModel> ls = new List<MappingViewModel>();
            try
            {


                ////try { 

                ////if (string.IsNullOrWhiteSpace(GlobalApp.JWT_Token) || !UTIL.GlobalApp.Authentication_Done) throw new Exception("Authorization Failed");
                //////  List<MappingViewModel> ls = new List<MappingViewModel>();
                ////var filter = new Fetch_CMS_Data_Filters { entity_id = pHotelId, date_from = pTimeFrom.ToString("MM/dd/yyyy hh:mm:ss"), hotel_id=pHotelId };
                //// var httpClient = new HttpClient();
                ////// serialize into json string
                ////httpClient.BaseAddress = new Uri(GlobalApp.API_URI);
                ////httpClient.DefaultRequestHeaders.Accept.Clear();
                ////httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                ////// JWT Token & call 
                ////httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + GlobalApp.JWT_Token);
                ////// Serialize JSON before sending to API
                ////var jsonContent = new StringContent(JsonSerializer.Serialize(filter), Encoding.UTF8, "application/json");

                ////var response = await httpClient.PostAsync(GlobalApp.API_URI + API.POST_CMS_DATA, jsonContent);
                ////response.EnsureSuccessStatusCode();
                ////var result = response.Content.ReadAsStringAsync();
                ////   //******************* Get Back ViewModels *******************************??
                ////    ResponseViewModel resp = JsonSerializer.Deserialize<ResponseViewModel>(result.Result, options);
                ////if (resp.status_code.ToString() == ((int)UTIL.Enums.ERROR_CODE.SUCCESS).ToString())
                ////    ls = JsonSerializer.Deserialize<List<MappingViewModel>>(resp.jsonData.ToString(), options);
                ////else
                ////    throw new Exception("Failed to get CMS data.");

                // hotelPreset = JsonSerializer.Deserialize<HotelViewModel>(result.Result);
                //************* Almas Comment Code ****************
                //MappingViewModel mappingViewModel = new MappingViewModel { id = 12089, entity_name = "Check In", status = 1, saves_status = 0, mode = 1, entity_Id = 2, entity_type = (int)UTIL.Enums.ENTITY_TYPES.SYNC };
                //List<EntityFieldViewModel> fields = new List<EntityFieldViewModel>();
                //FormViewModel form = new FormViewModel { id = 120912, pmspagetitle = "CheckIn", pmspageid = "CheckIn" };
                //fields.Add(new EntityFieldViewModel { sr = 1, fuid = "4abea0d0-ce8a-11eb-b5e9-ecb1d75edbb3", id = 1, pms_field_name = "reference", field_desc = "Booking Ref", pms_field_xpath = "reference",is_reference=1, default_value = "", data_type = 2, value = "BT78089",  mandatory = 1 });
                //fields.Add(new EntityFieldViewModel { sr = 2, fuid = "4abea140-ce8a-11eb-b5e9-ecb1d75edbb3", id = 2, pms_field_name = "hotel_id", field_desc = "Hotel", pms_field_xpath = "hotel_id", default_value = "", data_type = 1, value = "1", mandatory = 1 });
                //fields.Add(new EntityFieldViewModel { sr = 3, fuid = "4abea1aa-ce8a-11eb-b5e9-ecb1d75edbb3", id = 3, pms_field_name = "arrival_date", field_desc = "Arrival Date", pms_field_xpath = "arrival_date", default_value = "", data_type = 4, value = "2021-05-15 08:08:08", mandatory = 1 });
                //fields.Add(new EntityFieldViewModel { sr = 4, fuid = "4abea20e-ce8a-11eb-b5e9-ecb1d75edbb3", id = 4, pms_field_name = "departure_date", field_desc = "departure_date Date", pms_field_xpath = "departure_date", default_value = "", data_type = 4, value = "2021-05-18 08:08:08",  mandatory = 1 });
                //fields.Add(new EntityFieldViewModel { sr = 5, fuid = "4abea26e-ce8a-11eb-b5e9-ecb1d75edbb3", id = 5, pms_field_name = "cardholder_name", field_desc = "Card holder Name Date", pms_field_xpath = "cardholder_name", default_value = "Umar Khan", data_type = 2, value = "Umar Khan",  mandatory = 1 });
                //fields.Add(new EntityFieldViewModel { sr = 6, fuid = "4abea2d1-ce8a-11eb-b5e9-ecb1d75edbb3", id = 6, pms_field_name = "card_expiry", field_desc = "card expiry", pms_field_xpath = "card_expiry_date", default_value = "2024-09", data_type = 3, value = "2024-05-18",  mandatory = 1 });
                //fields.Add(new EntityFieldViewModel { sr = 7, fuid = "4abea32e-ce8a-11eb-b5e9-ecb1d75edbb3", id = 7, pms_field_name = "card_address", field_desc = "card address", pms_field_xpath = "card_address", default_value = "100 Ali Block Ittefaq Town, lahore", data_type = 2, value = "100 Ali Block Ittefaq Town, lahore", mandatory = 1 });
                //fields.Add(new EntityFieldViewModel { sr = 8, fuid = "4abea38e-ce8a-11eb-b5e9-ecb1d75edbb3", id = 8, pms_field_name = "phone_number", field_desc = "Phone", pms_field_xpath = "phone_number", default_value = "", data_type = 2, value = "+456093423", mandatory = 1 });
                //fields.Add(new EntityFieldViewModel { sr = 9, fuid = "4abea3eb-ce8a-11eb-b5e9-ecb1d75edbb3", id = 9, pms_field_name = "room_number", field_desc = "Room Number", pms_field_xpath = "room_number", default_value = "", data_type = 2, value = "101",  mandatory = 1 });
                //fields.Add(new EntityFieldViewModel { sr = 10, fuid = "4abea44c-ce8a-11eb-b5e9-ecb1d75edbb3", id = 10, pms_field_name = "email", field_desc = "Email", pms_field_xpath = "email", default_value = "", data_type = 2, value = "augsuriyah@servrhotel.com", mandatory = 1 });
                //fields.Add(new EntityFieldViewModel { sr = 11, fuid = "4abea4ac-ce8a-11eb-b5e9-ecb1d75edbb3", id = 11, pms_field_name = "card_number", field_desc = "card number", pms_field_xpath = "card_number", default_value = "", data_type = 2, value = "42424242424242",  mandatory = 1 });
                //fields.Add(new EntityFieldViewModel { sr = 12, fuid = "4abea50c-ce8a-11eb-b5e9-ecb1d75edbb3", id = 12, pms_field_name = "status", field_desc = "status", pms_field_xpath = "status", default_value = "", data_type = 2, value = "Check in",  mandatory = 1 });
                //fields.Add(new EntityFieldViewModel { sr = 13, fuid = "4abea569-ce8a-11eb-b5e9-ecb1d75edbb3", id = 13, pms_field_name = "updated_At", field_desc = "updated_At", pms_field_xpath = "updated_At", default_value = "", data_type = 4, value = "2021-05-18 08:08:08",  mandatory = 1 });
                //form.fields = fields;
                MappingViewModel mappingViewModel = new MappingViewModel { id = 2, entity_name = "Check In", entity_Id = 2, status = 1, saves_status = 0, reference = "LH21061121238681", entity_type = (int)UTIL.Enums.ENTITY_TYPES.SYNC, mode = 1, pmsformid = "form1", xpath = "" };
                List<EntityFieldViewModel> fields = new List<EntityFieldViewModel>();
                var form = new FormViewModel { id = 1, pmspagename = "form1" };
                fields.Add(new EntityFieldViewModel { sr = 0, fuid = "4abet801-ce8a-11eb-b5e9-ecb1d75edbb3", pms_field_name = "", field_desc = "", pms_field_xpath = "iframe_Edit reservation - LH21061121238681 _d314c5", default_value = "", data_type = 2, value = "LH21061121238681", control_type = 19, is_reference = 1, is_unmapped = 1, mandatory = 1 });
                fields.Add(new EntityFieldViewModel { sr = 0, fuid = "4abea802-ce8a-11eb-b5e9-ecb1d75edbb3", pms_field_name = "", field_desc = "", pms_field_xpath = "iframe_Edit reservation - LH21061521299115 _f3f7f3", default_value = "", data_type = 2, value = "", control_type = 19, is_reference = 0, is_unmapped = 1, mandatory = 1 });
                fields.Add(new EntityFieldViewModel { sr = 1, fuid = "", pms_field_name = "login-email", field_desc = "login email", parent_field_id = "", pms_field_xpath = "//input[@id='login-email']", is_reference = 1, is_unmapped = 3, default_value = "", data_type = 3, value = "", mandatory = 1 });
                fields.Add(new EntityFieldViewModel { sr = 1, fuid = "4abea803-ce8a-11eb-b5e9-ecb1d75edbb3", pms_field_name = "check_in_date_display", field_desc = "Arrival Date", parent_field_id = "4abea0d0-ce8a-11eb-b5e9-ecb1d75edbb3", pms_field_xpath = "//input[@id='check_in_date_display']", is_reference = 0, is_unmapped = 3, default_value = "", data_type = 3, value = "", mandatory = 1 });
                fields.Add(new EntityFieldViewModel { sr = 2, fuid = "4abea804-ce8a-11eb-b5e9-ecb1d75edbb3", pms_field_name = "check_out_date_display", field_desc = "departure Date", parent_field_id = "4abea0d0-ce8a-11eb-b5e9-ecb1d75edbb3", pms_field_xpath = "//input[@id='check_out_date_display']", is_reference = 0, is_unmapped = 3, default_value = "", data_type = 3, value = "", mandatory = 1 });
                fields.Add(new EntityFieldViewModel { sr = 3, fuid = "4abet805-ce8a-11eb-b5e9-ecb1d75edbb3", pms_field_name = "room_type_id", field_desc = "room type", parent_field_id = "4abet8d0-ce8a-11eb-b5e9-ecb1d75edbb3", pms_field_xpath = "//select[@name='reservation_room_types[][room_type_id]']", default_value = "100", data_type = 1, value = "", is_reference = 1, is_unmapped = 0, mandatory = 1 });
                fields.Add(new EntityFieldViewModel { sr = 4, fuid = "4abea806-ce8a-11eb-b5e9-ecb1d75edbb3", pms_field_name = "rate_plan_Id", field_desc = "rate", parent_field_id = "4abea0d0-ce8a-11eb-b5e9-ecb1d75edbb3", pms_field_xpath = "//select[@id='rate_plan_Id']", default_value = "", data_type = 1, value = "", is_reference = 0, is_unmapped = 0, mandatory = 1 });
                fields.Add(new EntityFieldViewModel { sr = 5, fuid = "4abea807-ce8a-11eb-b5e9-ecb1d75edbb3", pms_field_name = "room_id", field_desc = "Room Number", parent_field_id = "4abea0d0-ce8a-11eb-b5e9-ecb1d75edbb3", pms_field_xpath = "//select[@id='room_id']", default_value = "100", data_type = 1, value = "", is_reference = 0, is_unmapped = 0, mandatory = 1 });
                fields.Add(new EntityFieldViewModel { sr = 6, fuid = "4abet808-ce8a-11eb-b5e9-ecb1d75edbb3", pms_field_name = "number_adults", field_desc = "Number Adult ", parent_field_id = "4abet8d0-ce8a-11eb-b5e9-ecb1d75edbb3", pms_field_xpath = "//input[@id='number_adults']", default_value = "0", data_type = 2, value = "", is_reference = 0, is_unmapped = 0, mandatory = 1 });
                fields.Add(new EntityFieldViewModel { sr = 7, fuid = "4abet809-ce8a-11eb-b5e9-ecb1d75edbb3", pms_field_name = "number_children", field_desc = "number children", parent_field_id = "4abet8d0-ce8a-11eb-b5e9-ecb1d75edbb3", pms_field_xpath = "//input[@id='number_children']", default_value = "", data_type = 1, value = "", is_reference = 0, is_unmapped = 0, mandatory = 1 });
                fields.Add(new EntityFieldViewModel { sr = 8, fuid = "4abet810-ce8a-11eb-b5e9-ecb1d75edbb3", pms_field_name = "number_infants", field_desc = "number infants", parent_field_id = "4abet8d0-ce8a-11eb-b5e9-ecb1d75edbb3", pms_field_xpath = "//input[@id='number_infants']", default_value = "", data_type = 1, value = "", is_reference = 0, is_unmapped = 0, mandatory = 1 });
                fields.Add(new EntityFieldViewModel { sr = 9, fuid = "4abet811-ce8a-11eb-b5e9-ecb1d75edbb3", pms_field_name = "subtotal_amount", field_desc = "subtotal amount", parent_field_id = "4abet8d0-ce8a-11eb-b5e9-ecb1d75edbb3", pms_field_xpath = "//input[@id='subtotal_amount']", default_value = "", data_type = 1, value = "", is_reference = 0, is_unmapped = 0, mandatory = 1 });
                fields.Add(new EntityFieldViewModel { sr = 10, fuid = "4abet812-ce8a-11eb-b5e9-ecb1d75edbb3", pms_field_name = "extra_occupants_total", field_desc = "extra person", parent_field_id = "4abet8d0-ce8a-11eb-b5e9-ecb1d75edbb3", pms_field_xpath = "//input[@id='extra_occupants_total']", default_value = "", data_type = 1, value = "", is_reference = 0, is_unmapped = 0, mandatory = 1 });
                fields.Add(new EntityFieldViewModel { sr = 11, fuid = "4abet812-ce8a-11eb-b5e9-ecb1d75edbb3", pms_field_name = "rrt_discount", field_desc = "Discount", parent_field_id = "4abet8d0-ce8a-11eb-b5e9-ecb1d75edbb3", pms_field_xpath = "//input[@id='rrt_discount']", default_value = "100", data_type = 1, value = "", is_reference = 0, is_unmapped = 0, mandatory = 1 });
                fields.Add(new EntityFieldViewModel { sr = 12, fuid = "4abet813-ce8a-11eb-b5e9-ecb1d75edbb3", pms_field_name = "guest_first_name", field_desc = "First Name", parent_field_id = "4abet8d0-ce8a-11eb-b5e9-ecb1d75edbb3", pms_field_xpath = "//input[@name='first_name']", default_value = "0", data_type = 2, status = 0, value = "", is_reference = 0, is_unmapped = 0, mandatory = 0 });
                fields.Add(new EntityFieldViewModel { sr = 13, fuid = "4abet814-ce8a-11eb-b5e9-ecb1d75edbb3", pms_field_name = "guest_last_name", field_desc = "Last Name", parent_field_id = "4abet8d0-ce8a-11eb-b5e9-ecb1d75edbb3", pms_field_xpath = "//input[@name='last_name']", default_value = "0", data_type = 2, status = 0, value = "", is_reference = 0, is_unmapped = 0, mandatory = 1 });
                fields.Add(new EntityFieldViewModel { sr = 14, fuid = "4abet815-ce8a-11eb-b5e9-ecb1d75edbb3", pms_field_name = "guest_email", field_desc = "Email", parent_field_id = "4abet8d0-ce8a-11eb-b5e9-ecb1d75edbb3", pms_field_xpath = "//input[@id='guest_email']", default_value = "", data_type = 2, value = "", is_reference = 1, is_unmapped = 0, mandatory = 1 });
                fields.Add(new EntityFieldViewModel { sr = 15, fuid = "4abet816-ce8a-11eb-b5e9-ecb1d75edbb3", pms_field_name = "phone_number", field_desc = "Phone", parent_field_id = "4abet8d0-ce8a-11eb-b5e9-ecb1d75edbb3", pms_field_xpath = "//input[@id='phone_number']", default_value = "", data_type = 2, value = "", is_reference = 0, is_unmapped = 0, mandatory = 1 });
                fields.Add(new EntityFieldViewModel { sr = 16, fuid = "4abea817-ce8a-11eb-b5e9-ecb1d75edbb3", pms_field_name = "guest_organisation", field_desc = "Address", parent_field_id = "4abea0d0-ce8a-11eb-b5e9-ecb1d75edbb3", pms_field_xpath = "//input[@id='guest_organisation']", default_value = "", data_type = 2, value = "", is_reference = 0, is_unmapped = 0, mandatory = 1 });
                fields.Add(new EntityFieldViewModel { sr = 17, fuid = "4abea818-ce8a-11eb-b5e9-ecb1d75edbb3", pms_field_name = "guest_city", field_desc = "city", parent_field_id = "4abea0d0-ce8a-11eb-b5e9-ecb1d75edbb3", pms_field_xpath = "//select[@id='guest_city']", default_value = "", data_type = 2, value = "", is_reference = 1, is_unmapped = 0, mandatory = 0 });
                fields.Add(new EntityFieldViewModel { sr = 18, fuid = "4abet818-ce8a-11eb-b5e9-ecb1d75edbb3", pms_field_name = "payment_card_number", field_desc = "card number", parent_field_id = "4abet8d0-ce8a-11eb-b5e9-ecb1d75edbb3", pms_field_xpath = "//input[@id='payment_card_number']", default_value = "", data_type = 2, value = "", is_reference = 0, is_unmapped = 0, mandatory = 0 });
                fields.Add(new EntityFieldViewModel { sr = 19, fuid = "4abet819-ce8a-11eb-b5e9-ecb1d75edbb3", pms_field_name = "arrival_time", field_desc = "arrival time", parent_field_id = "4abet8d0-ce8a-11eb-b5e9-ecb1d75edbb3", pms_field_xpath = "//input[@id='arrival_time']", is_reference = 1, is_unmapped = 3, default_value = "", data_type = 3, value = "", mandatory = 1 });
                fields.Add(new EntityFieldViewModel { sr = 20, fuid = "4abet820-ce8a-11eb-b5e9-ecb1d75edbb3", pms_field_name = "guest_address", field_desc = "guest address", parent_field_id = "4abet8d0-ce8a-11eb-b5e9-ecb1d75edbb3", pms_field_xpath = "//input[@id='guest_address']", default_value = "", data_type = 2, value = "", is_reference = 0, is_unmapped = 0, mandatory = 1 });
                fields.Add(new EntityFieldViewModel { sr = 21, fuid = "4abea821-ce8a-11eb-b5e9-ecb1d75edbb3", pms_field_name = "guest_state", field_desc = "State", parent_field_id = "4abea0d0-ce8a-11eb-b5e9-ecb1d75edbb3", pms_field_xpath = "//input[@id='guest_state']", default_value = "", data_type = 2, value = "", is_reference = 1, is_unmapped = 0, mandatory = 0 });
                fields.Add(new EntityFieldViewModel { sr = 22, fuid = "4abea822-ce8a-11eb-b5e9-ecb1d75edbb3", pms_field_name = "guest_post_code", field_desc = "Postal code", parent_field_id = "4abea0d0-ce8a-11eb-b5e9-ecb1d75edbb3", pms_field_xpath = "//input[@id='guest_post_code']", default_value = "", data_type = 2, value = "", is_reference = 0, is_unmapped = 0, mandatory = 0 });
                fields.Add(new EntityFieldViewModel { sr = 23, fuid = "4abet823-ce8a-11eb-b5e9-ecb1d75edbb3", pms_field_name = "payment_card_expiry_month", parent_field_id = "4abet8d0-ce8a-11eb-b5e9-ecb1d75edbb3", field_desc = "Expiry Month", pms_field_xpath = "//input[@id='payment_card_expiry_month']", default_value = "", data_type = 3, value = "", is_reference = 1, is_unmapped = 0, mandatory = 0 });
                fields.Add(new EntityFieldViewModel { sr = 24, fuid = "4abet824-ce8a-11eb-b5e9-ecb1d75edbb3", pms_field_name = "payment_card_expiry_year", parent_field_id = "4abet8d0-ce8a-11eb-b5e9-ecb1d75edbb3", field_desc = "Expiry Year", pms_field_xpath = "//input[@id='payment_card_expiry_year']", default_value = "", data_type = 3, is_reference = 0, is_unmapped = 0, value = "", mandatory = 0 });
                fields.Add(new EntityFieldViewModel { sr = 25, fuid = "4abet825-ce8a-11eb-b5e9-ecb1d75edbb3", pms_field_name = "referral", parent_field_id = "4abet8d0-ce8a-11eb-b5e9-ecb1d75edbb3", field_desc = "", pms_field_xpath = "//select[@id='referral']", default_value = "", data_type = 2, value = "", is_reference = 0, is_unmapped = 0, mandatory = 0 });
                fields.Add(new EntityFieldViewModel { sr = 26, fuid = "4abet826-ce8a-11eb-b5e9-ecb1d75edbb3", pms_field_name = "guest_address2", field_desc = "guest address 2", parent_field_id = "4abet8d0-ce8a-11eb-b5e9-ecb1d75edbb3", pms_field_xpath = "//input[@id='guest_address2']", default_value = "", data_type = 2, value = "", is_reference = 0, is_unmapped = 0, mandatory = 1 });
                fields.Add(new EntityFieldViewModel { sr = 27, fuid = "4abet827-ce8a-11eb-b5e9-ecb1d75edbb3", pms_field_name = "guest_country", field_desc = "guest country", parent_field_id = "4abet8d0-ce8a-11eb-b5e9-ecb1d75edbb3", pms_field_xpath = "//select[@id='guest_country']", default_value = "", data_type = 2, value = "", is_reference = 0, is_unmapped = 0, mandatory = 0 });
                fields.Add(new EntityFieldViewModel { sr = 28, fuid = "4abet828-ce8a-11eb-b5e9-ecb1d75edbb3", pms_field_name = "payment_card_name", field_desc = "payment card name", parent_field_id = "4abet8d0-ce8a-11eb-b5e9-ecb1d75edbb3", pms_field_xpath = "//input[@id='payment_card_name']", default_value = "", data_type = 2, value = "", is_reference = 0, is_unmapped = 0, mandatory = 1 });
                fields.Add(new EntityFieldViewModel { sr = 29, fuid = "4abet829-ce8a-11eb-b5e9-ecb1d75edbb3", pms_field_name = "guest_id_document_type", parent_field_id = "4abet8d0-ce8a-11eb-b5e9-ecb1d75edbb3", field_desc = "Documents Type", pms_field_xpath = "//select[@id='guest_id_document_type']", default_value = "", data_type = 4, is_reference = 0, is_unmapped = 0, value = "", status = 0, mandatory = 1 });
                fields.Add(new EntityFieldViewModel { sr = 30, fuid = Guid.NewGuid().ToString(), pms_field_name = "guest_id_number", parent_field_id = "4abet8d0-ce8a-11eb-b5e9-ecb1d75edbb3", field_desc = "Id Number", pms_field_xpath = "//input[@id='guest_id_number']", default_value = "", data_type = 2, is_reference = 0, is_unmapped = 0, value = "", mandatory = 1 });
                fields.Add(new EntityFieldViewModel { sr = 31, fuid = Guid.NewGuid().ToString(), pms_field_name = "guest_comments", parent_field_id = "4abet8d0-ce8a-11eb-b5e9-ecb1d75edbb3", field_desc = "Comments", pms_field_xpath = "//input[@id='guest_comments']", default_value = "", data_type = 2, is_reference = 0, is_unmapped = 0, value = "", mandatory = 0 });
                fields.Add(new EntityFieldViewModel { sr = 32, fuid = Guid.NewGuid().ToString(), pms_field_name = "li_Checked-in", parent_field_id = "4abea0d0-ce8a-11eb-b5e9-ecb1d75edbb3", field_desc = "Checked In", pms_field_xpath = "//div[@id='popover889402']/div[2]/ul/li[2]", default_value = "", data_type = 2, is_reference = 0, is_unmapped = 0, value = "", mandatory = 0 });
                fields.Add(new EntityFieldViewModel { sr = 33, fuid = Guid.NewGuid().ToString(), pms_field_name = "li_Checked-out", parent_field_id = "4abea0d0-ce8a-11eb-b5e9-ecb1d75edbb3", field_desc = "Checked Out", pms_field_xpath = "//div[@id='popover171674']/div[2]/ul/li[3]", default_value = "", data_type = 2, is_reference = 0, is_unmapped = 0, value = "", mandatory = 0 });
                fields.Add(new EntityFieldViewModel { sr = 34, fuid = Guid.NewGuid().ToString(), pms_field_name = "li_Confirmed", parent_field_id = "4abea0d0-ce8a-11eb-b5e9-ecb1d75edbb3", field_desc = "Confirmed", pms_field_xpath = "//div[@id='popover843235']/div[2]/ul/li", default_value = "", data_type = 2, is_reference = 0, is_unmapped = 0, value = "", mandatory = 0 });
                fields.Add(new EntityFieldViewModel { sr = 35, fuid = Guid.NewGuid().ToString(), pms_field_name = "", parent_field_id = "4abea0d0-ce8a-11eb-b5e9-ecb1d75edbb3", field_desc = "Close", pms_field_xpath = "(//button[@type='button'])[31]", default_value = "", data_type = 2, is_reference = 1, is_unmapped = 0, value = "", mandatory = 0 });



                //fields.Add(new EntityFieldViewModel { sr = 1, fuid = "4abea140-ce8a-11eb-b5e9-ecb1d75edbb3", pms_field_name = "details", field_desc = "Booking Ref", parent_field_id = "4abet8d0-ce8a-11eb-b5e9-ecb1d75edbb3", pms_field_expression = "//input[@id='details']", pms_field_xpath = "", is_reference = 1, is_unmapped = 3, default_value = "", data_type = 2, value = "", mandatory = 1 });
                //fields.Add(new EntityFieldViewModel { sr = 2, fuid = "4abea1aa-ce8a-11eb-b5e9-ecb1d75edbb3", pms_field_name = "hotel_id", field_desc = "Hotel", parent_field_id = "4abet8d0-ce8a-11eb-b5e9-ecb1d75edbb3", pms_field_expression = "//select[@id='hotel_id']", pms_field_xpath = "", is_reference = 1, is_unmapped = 3, default_value = "0", data_type = 1, value = "", mandatory = 1 });


                fields.Add(new EntityFieldViewModel { sr = 20, fuid = Guid.NewGuid().ToString(), pms_field_name = "btnSubmit", parent_field_id = "4abea0d0-ce8a-11eb-b5e9-ecb1d75edbb3", field_desc = "Submit", pms_field_xpath = "//a[contains(text(),'Submit')]", default_value = "", data_type = 0, control_type = 17, is_reference = 0, is_unmapped = 0, value = "Submit", mandatory = 0 });
                form.fields = fields;
                List<FormViewModel> frmls = new List<FormViewModel>();
                frmls.Add(form);
                mappingViewModel.forms = frmls;
                ls.Add(mappingViewModel);

                MappingViewModel mappingViewModelweb = new MappingViewModel { id = 2, entity_name = "Check In", status = 1, saves_status = 0, mode = 1, entity_Id = 2, reference = "LH21061521299115", entity_type = (int)UTIL.Enums.ENTITY_TYPES.SYNC };
                List<EntityFieldViewModel> fieldsweb = new List<EntityFieldViewModel>();
                FormViewModel formwev = new FormViewModel { id = 120912, pmspagetitle = "CheckIn", pmspageid = "CheckIn" };
                fieldsweb.Add(new EntityFieldViewModel { sr = 1, fuid = "4abea0d0-ce8a-11eb-b5e9-ecb1d75edbb3", id = 1, pms_field_name = "details", field_desc = "Booking Ref", pms_field_expression = "", pms_field_xpath = "iframe[@class='el-dialog_body']/iframe[@class='reservation-modal-popup_frame']", is_reference = 1, is_unmapped = 0, default_value = "", data_type = 2, control_type = 1, value = "LH21061521299115", mandatory = 1 });
                fieldsweb.Add(new EntityFieldViewModel { sr = 2, fuid = "4abea140-ce8a-11eb-b5e9-ecb1d75edbb3", id = 2, pms_field_name = "hotel_id", field_desc = "Hotel", pms_field_expression = "", pms_field_xpath = "iframe[@class='el-dialog_body']/iframe[@class='reservation-modal-popup_frame']", is_reference = 0, is_unmapped = 0, default_value = "0", data_type = 1, value = "", control_type = 3, mandatory = 1 });
                fieldsweb.Add(new EntityFieldViewModel { sr = 3, fuid = "4abea1aa-ce8a-11eb-b5e9-ecb1d75edbb3", id = 3, pms_field_name = "check_in_date_display", field_desc = "Arrival Date", pms_field_xpath = "iframe[@class='el-dialog_body']/iframe[@class='reservation-modal-popup_frame']", is_reference = 0, is_unmapped = 0, default_value = "", data_type = 4, value = "", control_type = 3, mandatory = 1 });
                fieldsweb.Add(new EntityFieldViewModel { sr = 4, fuid = "4abea20e-ce8a-11eb-b5e9-ecb1d75edbb3", id = 4, pms_field_name = "check_out_date_display", field_desc = "departure Date", pms_field_xpath = "iframe[@class='el-dialog_body']/iframe[@class='reservation-modal-popup_frame']", is_reference = 0, is_unmapped = 0, default_value = "", data_type = 4, value = "", mandatory = 1 });
                fieldsweb.Add(new EntityFieldViewModel { sr = 5, fuid = "4abea26e-ce8a-11eb-b5e9-ecb1d75edbb3", id = 5, pms_field_name = "guest_phone_number", field_desc = "Phone", pms_field_xpath = "iframe[@class='el-dialog_body']/iframe[@class='reservation-modal-popup_frame']", default_value = "", data_type = 2, value = "", is_reference = 0, is_unmapped = 0, mandatory = 1 });
                fieldsweb.Add(new EntityFieldViewModel { sr = 6, fuid = "4abea2d1-ce8a-11eb-b5e9-ecb1d75edbb3", id = 6, pms_field_name = "rate_plan_Id", field_desc = "rate", pms_field_xpath = "iframe[@class='el-dialog_body']/iframe[@class='reservation-modal-popup_frame']", default_value = "", data_type = 2, value = "", is_reference = 0, is_unmapped = 0, mandatory = 1 });
                fieldsweb.Add(new EntityFieldViewModel { sr = 7, fuid = "4abea32e-ce8a-11eb-b5e9-ecb1d75edbb3", id = 7, pms_field_name = "room_id", field_desc = "Room Number", pms_field_xpath = "iframe[@class='el-dialog_body']/iframe[@class='reservation-modal-popup_frame']", default_value = "0", data_type = 2, value = "", is_reference = 0, is_unmapped = 0, mandatory = 1 });
                fieldsweb.Add(new EntityFieldViewModel { sr = 8, fuid = "4abea38e-ce8a-11eb-b5e9-ecb1d75edbb3", id = 8, pms_field_name = "number_adults", field_desc = "Number Adult ", pms_field_xpath = "iframe[@class='el-dialog_body']/iframe[@class='reservation-modal-popup_frame']", default_value = "0", data_type = 2, value = "", is_reference = 0, is_unmapped = 0, mandatory = 1 });
                fieldsweb.Add(new EntityFieldViewModel { sr = 9, fuid = "4abea3eb-ce8a-11eb-b5e9-ecb1d75edbb3", id = 9, pms_field_name = "guest_first_name", field_desc = "First Name", pms_field_xpath = "iframe[@class='el-dialog_body']/iframe[@class='reservation-modal-popup_frame']", default_value = "0", data_type = 2, value = "", is_reference = 0, is_unmapped = 0, mandatory = 1 });
                fieldsweb.Add(new EntityFieldViewModel { sr = 10, fuid = "4abea44c-ce8a-11eb-b5e9-ecb1d75edbb3", id = 10, pms_field_name = "guest_last_name", field_desc = "Last Name", pms_field_xpath = "iframe[@class='el-dialog_body']/iframe[@class='reservation-modal-popup_frame']", default_value = "0", data_type = 2, value = "", is_reference = 0, is_unmapped = 0, mandatory = 1 });
                fieldsweb.Add(new EntityFieldViewModel { sr = 11, fuid = "4abea4ac-ce8a-11eb-b5e9-ecb1d75edbb3", id = 11, pms_field_name = "guest_email", field_desc = "Email", pms_field_xpath = "iframe[@class='el-dialog_body']/iframe[@class='reservation-modal-popup_frame']", default_value = "", data_type = 2, value = "", is_reference = 0, is_unmapped = 0, mandatory = 1 });
                fieldsweb.Add(new EntityFieldViewModel { sr = 12, fuid = "4abea50c-ce8a-11eb-b5e9-ecb1d75edbb3", id = 12, pms_field_name = "guest_organisation", field_desc = "Address", pms_field_xpath = "iframe[@class='el-dialog_body']/iframe[@class='reservation-modal-popup_frame']", default_value = "", data_type = 2, value = "", is_reference = 0, is_unmapped = 3, mandatory = 1 });
                fieldsweb.Add(new EntityFieldViewModel { sr = 13, fuid = "4abea569-ce8a-11eb-b5e9-ecb1d75edbb3", id = 13, pms_field_name = "guest_country", field_desc = "Country", pms_field_xpath = "iframe[@class='el-dialog_body']/iframe[@class='reservation-modal-popup_frame']", default_value = "", data_type = 2, value = "", is_reference = 0, is_unmapped = 0, control_type = 2, mandatory = 1 });
                fieldsweb.Add(new EntityFieldViewModel { sr = 14, fuid = "4abea5c6-ce8a-11eb-b5e9-ecb1d75edbb3", id = 14, pms_field_name = "payment_card_name", field_desc = "Card Name", pms_field_xpath = "iframe[@class='el-dialog_body']/iframe[@class='reservation-modal-popup_frame']", default_value = "", data_type = 2, value = "", is_reference = 0, is_unmapped = 0, control_type = 1, mandatory = 1 });
                fieldsweb.Add(new EntityFieldViewModel { sr = 15, fuid = "4abea62a-ce8a-11eb-b5e9-ecb1d75edbb3", id = 15, pms_field_name = "payment_card_expiry_month", field_desc = "Expiry Month", pms_field_xpath = "iframe[@class='el-dialog_body']/iframe[@class='reservation-modal-popup_frame']", default_value = "", data_type = 2, value = "", is_reference = 0, is_unmapped = 0, control_type = 1, mandatory = 1 });
                fieldsweb.Add(new EntityFieldViewModel { sr = 16, fuid = "4abea68a-ce8a-11eb-b5e9-ecb1d75edbb3", id = 16, pms_field_name = "payment_card_expiry_year", field_desc = "Expiry Year", pms_field_xpath = "iframe[@class='el-dialog_body']/iframe[@class='reservation-modal-popup_frame']", default_value = "", data_type = 4, is_reference = 0, is_unmapped = 0, value = "", control_type = 1, mandatory = 1 });
                fieldsweb.Add(new EntityFieldViewModel { sr = 17, fuid = "4abea6e7-ce8a-11eb-b5e9-ecb1d75edbb3", id = 17, pms_field_name = "guest_id_document_type", field_desc = "Documents Type", pms_field_xpath = "iframe[@class='el-dialog_body']/iframe[@class='reservation-modal-popup_frame']", default_value = "", data_type = 4, is_reference = 0, is_unmapped = 0, value = "", control_type = 1, mandatory = 1 });
                fieldsweb.Add(new EntityFieldViewModel { sr = 18, fuid = "4abea747-ce8a-11eb-b5e9-ecb1d75edbb3", id = 18, pms_field_name = "guest_id_number", field_desc = "Id Number", pms_field_xpath = "iframe[@class='el-dialog_body']/iframe[@class='reservation-modal-popup_frame']", default_value = "", data_type = 4, is_reference = 0, is_unmapped = 0, value = "", control_type = 1, mandatory = 1 });
                fieldsweb.Add(new EntityFieldViewModel { sr = 19, fuid = Guid.NewGuid().ToString(), id = 19, pms_field_name = "guest_comments", field_desc = "Comments", pms_field_xpath = "iframe[@class='el-dialog_body']/iframe[@class='reservation-modal-popup_frame']", default_value = "", data_type = 4, is_reference = 0, is_unmapped = 0, value = "", control_type = 1, mandatory = 1 });
                formwev.fields = fields;
                List<FormViewModel> frmlsw = new List<FormViewModel>();
                frmlsw.Add(form);
                mappingViewModelweb.forms = frmlsw;
                ls.Add(mappingViewModelweb);


                MappingViewModel mappingViewModel2 = new MappingViewModel { id = 4, entity_name = "Check Out", status = 1, entity_Id = 4, saves_status = 0, entity_type = (int)UTIL.Enums.ENTITY_TYPES.SYNC, mode = 3, reference = "BT120089", xpath = "/Pane[@Name=\u0022Desktop 1\u0022][@ClassName=\u0022#32769\u0022]/Window[@AutomationId=\u0022PMSForm\u0022][@Name=\u0022PMS Form\u0022]" };
                FormViewModel form1 = new FormViewModel { id = 12092, pmspagetitle = "Check Out", pmspageid = "Check Out" };
                fields = new List<EntityFieldViewModel>();

                fields.Add(new EntityFieldViewModel { sr = 1, fuid = "4abeac17-ce8a-11eb-b5e9-ecb1d75edbb3", pms_field_name = "reference", field_desc = "Booking Ref", pms_field_xpath = "reference", default_value = "", is_reference = 1, data_type = 2, value = "BT120089", mandatory = 1 });
                fields.Add(new EntityFieldViewModel { sr = 2, fuid = "4abeac74-ce8a-11eb-b5e9-ecb1d75edbb3", pms_field_name = "hotel_id", field_desc = "Hotel", pms_field_xpath = "hotel_id", default_value = "", is_reference = 0, data_type = 1, value = "1", mandatory = 1 });
                fields.Add(new EntityFieldViewModel { sr = 3, fuid = "4abeacd1-ce8a-11eb-b5e9-ecb1d75edbb3", pms_field_name = "arrival_date", field_desc = "Arrival Date", pms_field_xpath = "arrival_date", default_value = "", is_reference = 0, data_type = 4, value = "2021-05-18 08:08:08", mandatory = 1 });
                fields.Add(new EntityFieldViewModel { sr = 4, fuid = "4abead2e-ce8a-11eb-b5e9-ecb1d75edbb3", pms_field_name = "departure_date", field_desc = "departure_date Date", pms_field_xpath = "departure_date", default_value = "", is_reference = 0, data_type = 4, value = "2021-05-21 08:08:08", mandatory = 1 });
                fields.Add(new EntityFieldViewModel { sr = 5, fuid = "4abead8b-ce8a-11eb-b5e9-ecb1d75edbb3", pms_field_name = "cardholder_name", field_desc = "Card holder Name Date", pms_field_xpath = "cardholder_name", is_reference = 0, default_value = "Umar Khan", data_type = 2, value = "Asim Sohail", mandatory = 1 });
                fields.Add(new EntityFieldViewModel { sr = 6, fuid = Guid.NewGuid().ToString(), pms_field_name = "card_expiry", field_desc = "card expiry", pms_field_xpath = "card_expiry_date", default_value = "2024-09", is_reference = 0, data_type = 3, value = "2025-06-18", mandatory = 1 });
                fields.Add(new EntityFieldViewModel { sr = 7, fuid = Guid.NewGuid().ToString(), pms_field_name = "card_address", field_desc = "card address", pms_field_xpath = "card_address", default_value = "101 B Johar Town, lahore", is_reference = 0, data_type = 2, value = "100 Ali Block Ittefaq Town, lahore", mandatory = 1 });
                fields.Add(new EntityFieldViewModel { sr = 8, fuid = Guid.NewGuid().ToString(), pms_field_name = "phone_number", field_desc = "Phone", pms_field_xpath = "phone_number", default_value = "", data_type = 2, is_reference = 0, value = "+923001111111", mandatory = 1 });
                fields.Add(new EntityFieldViewModel { sr = 9, fuid = Guid.NewGuid().ToString(), pms_field_name = "room_number", field_desc = "Room Number", pms_field_xpath = "room_number", default_value = "", is_reference = 0, data_type = 2, value = "102", mandatory = 1 });
                fields.Add(new EntityFieldViewModel { sr = 10, fuid = Guid.NewGuid().ToString(), pms_field_name = "email", field_desc = "Email", pms_field_xpath = "email", default_value = "", data_type = 2, is_reference = 0, value = "temp@servrhotel.com", mandatory = 1 });
                fields.Add(new EntityFieldViewModel { sr = 11, fuid = Guid.NewGuid().ToString(), pms_field_name = "card_number", field_desc = "card number", pms_field_xpath = "card_number", default_value = "", is_reference = 0, data_type = 2, value = "4141414141414141", mandatory = 1 });
                fields.Add(new EntityFieldViewModel { sr = 12, fuid = Guid.NewGuid().ToString(), id = 12, pms_field_name = "status", field_desc = "status", pms_field_xpath = "status", default_value = "", is_reference = 0, data_type = 2, value = "Check in", mandatory = 1 });
                fields.Add(new EntityFieldViewModel { sr = 13, fuid = Guid.NewGuid().ToString(), id = 13, pms_field_name = "updated_At", field_desc = "updated_At", pms_field_xpath = "updated_At", default_value = "", is_reference = 0, data_type = 4, value = "2021-05-18 08:08:08", mandatory = 1 });
                form1.fields = fields;
                List<FormViewModel> frmls1 = new List<FormViewModel>();
                frmls1.Add(form1);
                mappingViewModel2.forms = frmls1;
                ls.Add(mappingViewModel2);


                MappingViewModel mappingViewModel3 = new MappingViewModel { id = 3, entity_name = "Billing Details", entity_Id = 3, status = 1, saves_status = 0, entity_type = (int)UTIL.Enums.ENTITY_TYPES.SYNC, mode = 3, reference = "Sv129012", xpath = "/Pane[@Name=\u0022Desktop 1\u0022][@ClassName=\u0022#32769\u0022]/Window[@AutomationId=\u0022PMSForm\u0022][@Name=\u0022PMS Form\u0022]" };
                FormViewModel form2 = new FormViewModel { id = 3, pmspagetitle = "Billing Details", pmspageid = "Billing Details" };
                fields = new List<EntityFieldViewModel>();

                fields.Add(new EntityFieldViewModel { sr = 1, fuid = "4abea97f-ce8a-11eb-b5e9-ecb1d75edbb3", id = 1, pms_field_name = "reference", field_desc = "Booking Ref", pms_field_xpath = "reference", default_value = "", is_reference = 1, data_type = 2, value = "Sv129012", mandatory = 1 });
                fields.Add(new EntityFieldViewModel { sr = 2, fuid = "4abea9dc-ce8a-11eb-b5e9-ecb1d75edbb3", id = 2, pms_field_name = "hotel_id", field_desc = "Hotel", pms_field_xpath = "hotel_id", default_value = "", data_type = 1, is_reference = 0, value = "1", mandatory = 1 });
                fields.Add(new EntityFieldViewModel { sr = 3, fuid = "4abeaa3c-ce8a-11eb-b5e9-ecb1d75edbb3", id = 3, pms_field_name = "arrival_date", field_desc = "Arrival Date", pms_field_xpath = "arrival_date", default_value = "", is_reference = 0, data_type = 4, value = "2021-05-18 08:08:08", mandatory = 1 });
                fields.Add(new EntityFieldViewModel { sr = 4, fuid = "4abeaa9c-ce8a-11eb-b5e9-ecb1d75edbb3", id = 4, pms_field_name = "departure_date", field_desc = "departure_date Date", pms_field_xpath = "departure_date", is_reference = 0, default_value = "", data_type = 4, value = "2021-05-21 08:08:08", mandatory = 1 });
                fields.Add(new EntityFieldViewModel { sr = 5, fuid = "4abeaaf9-ce8a-11eb-b5e9-ecb1d75edbb3", id = 5, pms_field_name = "cardholder_name", field_desc = "Card holder Name Date", pms_field_xpath = "cardholder_name", is_reference = 0, default_value = "Umar Khan", data_type = 2, value = "Asim Sohail", mandatory = 1 });
                fields.Add(new EntityFieldViewModel { sr = 6, fuid = "4abeab56-ce8a-11eb-b5e9-ecb1d75edbb3", id = 6, pms_field_name = "card_expiry", field_desc = "card expiry", pms_field_xpath = "card_expiry_date", is_reference = 0, default_value = "2024-09", data_type = 3, value = "2025-06-18", mandatory = 1 });
                fields.Add(new EntityFieldViewModel { sr = 7, fuid = "4abeabb6-ce8a-11eb-b5e9-ecb1d75edbb3", id = 7, pms_field_name = "card_address", field_desc = "card address", pms_field_xpath = "card_address", is_reference = 0, default_value = "101 B Johar Town, lahore", data_type = 2, value = "86 E AEH lahore", mandatory = 1 });
                fields.Add(new EntityFieldViewModel { sr = 8, fuid = Guid.NewGuid().ToString(), pms_field_name = "phone_number", field_desc = "Phone", pms_field_xpath = "phone_number", default_value = "", is_reference = 0, data_type = 2, value = "+923001111111", mandatory = 1 });
                fields.Add(new EntityFieldViewModel { sr = 9, fuid = Guid.NewGuid().ToString(), pms_field_name = "room_number", field_desc = "Room Number", pms_field_xpath = "room_number", default_value = "", is_reference = 0, data_type = 2, value = "102", mandatory = 1 });
                fields.Add(new EntityFieldViewModel { sr = 10, fuid = Guid.NewGuid().ToString(), pms_field_name = "email", field_desc = "Email", pms_field_xpath = "email", default_value = "", is_reference = 0, data_type = 2, value = "temp@servrhotel.com", mandatory = 1 });
                fields.Add(new EntityFieldViewModel { sr = 11, fuid = Guid.NewGuid().ToString(), pms_field_name = "card_number", field_desc = "card number", pms_field_xpath = "card_number", default_value = "", is_reference = 0, data_type = 2, value = "4141414141414141", mandatory = 1 });
                fields.Add(new EntityFieldViewModel { sr = 12, fuid = Guid.NewGuid().ToString(), pms_field_name = "status", field_desc = "status", pms_field_xpath = "status", default_value = "", data_type = 2, is_reference = 0, value = "Check in", mandatory = 1 });
                fields.Add(new EntityFieldViewModel { sr = 13, fuid = Guid.NewGuid().ToString(), pms_field_name = "updated_At", field_desc = "updated_At", pms_field_xpath = "updated_At", default_value = "", is_reference = 0, data_type = 4, value = "2021-05-18 08:08:08", mandatory = 1 });
                form2.fields = fields;
                List<FormViewModel> frmls2 = new List<FormViewModel>();
                frmls2.Add(form2);
                mappingViewModel3.forms = frmls2;
                ls.Add(mappingViewModel3);

                MappingViewModel conceirgemodel = new MappingViewModel { id = 6, entity_name = "Conceirge", entity_Id = 6, status = 1, saves_status = 0, entity_type = (int)UTIL.Enums.ENTITY_TYPES.STATS, mode = 3, xpath = "/Pane[@Name=\u0022Desktop 1\u0022][@ClassName=\u0022#32769\u0022]/Window[@AutomationId=\u0022PMSForm\u0022][@Name=\u0022PMS Form\u0022]" };
                FormViewModel conciergefrm = new FormViewModel { id = 6, pmspagetitle = "Conceirge", pmspageid = "Billing Details" };
                fields = new List<EntityFieldViewModel>();

                fields.Add(new EntityFieldViewModel { sr = 1, fuid = "4abeae4e-ce8a-11eb-b5e9-ecb1d75edbb3", pms_field_name = "reference", field_desc = "Booking Ref", pms_field_xpath = "reference", default_value = "", data_type = 2, value = "2", mandatory = 1 });
                conciergefrm.fields = fields;
                List<FormViewModel> frmConls = new List<FormViewModel>();
                frmConls.Add(conciergefrm);
                conceirgemodel.forms = frmConls;
                ls.Add(conceirgemodel);



                MappingViewModel spamodel = new MappingViewModel { id = 5, entity_name = "spa", entity_Id = 5, status = 1, saves_status = 0, entity_type = (int)UTIL.Enums.ENTITY_TYPES.STATS, mode = 3, xpath = "/Pane[@Name=\u0022Desktop 1\u0022][@ClassName=\u0022#32769\u0022]/Window[@AutomationId=\u0022PMSForm\u0022][@Name=\u0022PMS Form\u0022]" };
                FormViewModel spafrm = new FormViewModel { id = 120102, pmspagetitle = "spa", pmspageid = "spa Details" };
                fields = new List<EntityFieldViewModel>();

                fields.Add(new EntityFieldViewModel { sr = 1, fuid = "4abeadeb-ce8a-11eb-b5e9-ecb1d75edbb3", pms_field_name = "reference", field_desc = "Booking Ref", pms_field_xpath = "reference", default_value = "", data_type = 2, value = "2", mandatory = 1 });
                spafrm.fields = fields;
                List<FormViewModel> frmspals = new List<FormViewModel>();
                frmspals.Add(spafrm);
                spamodel.forms = frmspals;


                ls.Add(spamodel);


                MappingViewModel resModel = new MappingViewModel { id = 8, entity_name = "Restauran", entity_Id = 8, status = 1, saves_status = 0, entity_type = (int)UTIL.Enums.ENTITY_TYPES.STATS, mode = 3, xpath = "/Pane[@Name=\u0022Desktop 1\u0022][@ClassName=\u0022#32769\u0022]/Window[@AutomationId=\u0022PMSForm\u0022][@Name=\u0022PMS Form\u0022]" };
                FormViewModel resfrm = new FormViewModel { id = 1292, pmspagetitle = "Restauran", pmspageid = "Restauran" };
                fields = new List<EntityFieldViewModel>();

                fields.Add(new EntityFieldViewModel { sr = 1, id = 1, fuid = "4abetthf-ce8a-11eb-b5e9-ecb1d75edbb3", pms_field_name = "reference", field_desc = "Booking Ref", pms_field_xpath = "reference", default_value = "", data_type = 2, value = "4", mandatory = 1 });
                resfrm.fields = fields;
                List<FormViewModel> frmsresls = new List<FormViewModel>();
                frmsresls.Add(resfrm);
                resModel.forms = frmsresls;
                ls.Add(resModel);

            }
            catch (Exception ex)
            {
                if (ex.ToString().ToLower().Contains(BDUConstants.API_UNATHENTICATED))
                {
                    UTIL.GlobalApp.JWT_Token = string.Empty;
                    UTIL.GlobalApp.Authentication_Done = false;
                }
            }
            return ls;
        }
        public async Task<List<MappingViewModel>> getCMSDataService(int pHotelId, DateTime pTimeFrom)
        {
            EntityData data = new EntityData();
            List<MappingViewModel> cmsDatals = new List<MappingViewModel>();
            try
            {
                ////try { 
                if (string.IsNullOrWhiteSpace(GlobalApp.JWT_Token) || !UTIL.GlobalApp.Authentication_Done) throw new Exception("Authorization Failed");
                //  List<MappingViewModel> ls = new List<MappingViewModel>();
                // var filter = new Fetch_CMS_Data_Filters { time = pTimeFrom.ToUniversalTime().ToString(UTIL.GlobalApp.date_time_format), hotel_id = pHotelId };
                var filter = new Fetch_CMS_Data_Filters { time = pTimeFrom.ToString(UTIL.GlobalApp.date_time_format), hotel_id = pHotelId };
                var httpClient = new HttpClient();
                // serialize into json string
                httpClient.BaseAddress = new Uri(GlobalApp.API_URI);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // JWT Token & call 
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + GlobalApp.JWT_Token);
                // Serialize JSON before sending to API
                var jsonContent = new StringContent(JsonSerializer.Serialize(filter), Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(GlobalApp.API_URI + API.POST_CMS_DATA, jsonContent);
                response.EnsureSuccessStatusCode();
                var result = response.Content.ReadAsStringAsync();
                //******************* Get Back ViewModels *******************************??
                ResponseViewModel resp = JsonSerializer.Deserialize<ResponseViewModel>(result.Result, options);
                if (resp.status_code.ToString() == ((int)UTIL.Enums.ERROR_CODE.SUCCESS).ToString())
                    data = JsonSerializer.Deserialize<EntityData>(resp.jsonData.ToString(), options);
                if (data != null && data.jsonData != null)
                {
                    List<MappingViewModel> mData = API.PRESETS.mappings;
                    var retData = data.jsonData.Where(x => x.entity_id > 0 && x.data != null);
                    //******************************  Entity Type UTIL.Enums.ENTITIES.CHECKIN *******************************//
                    foreach (CoreDataEntity ets in retData)
                    {

                        List<CoreDataEntityRecords> fldsls = ets.data.Where(x => x.data != null).ToList();
                        foreach (CoreDataEntityRecords record in fldsls)
                        {
                            MappingViewModel dMapping = mData.Where(x => x.entity_Id == ets.entity_id).FirstOrDefault().DCopy();
                            dMapping.id = ets.entity_id;
                            dMapping.entity_type = UTIL.GlobalApp.Get_Entity_Type(ets.entity_id);
                            // var myEnumDescriptions = StringValueOf((UTIL.Enums.ENTITIES)ets.entity_id);
                            dMapping.entity_name = StringValueOf((UTIL.Enums.ENTITIES)ets.entity_id);
                            dMapping.mode = 2;
                            dMapping.reference = "" + record.id;
                            if (record.data != null)
                            {
                                List<CoreFieldData> DFields = record.data ;
                                // cmsData.id = Convert.ToInt64(flds.FirstOrDefault().id);
                                foreach (FormViewModel f in dMapping.forms)
                                {
                                    var qry = from ffs in f.fields
                                              join dfs in DFields on ffs.fuid equals dfs.fuid
                                              select new { ffs, dvalue = dfs.value };
                                    foreach (var rec in qry)
                                    {
                                        if (!string.IsNullOrWhiteSpace(rec.dvalue) && (rec.ffs.data_type == (int)UTIL.Enums.DATA_TYPES.DATE || rec.ffs.data_type == (int)UTIL.Enums.DATA_TYPES.DATETIME))
                                        {
                                            try
                                            {
                                                // rec.ffs.value = Convert.ToDateTime(rec.dvalue).ToString(rec.ffs.format);
                                                rec.ffs.value = Convert.ToDateTime(rec.dvalue).ToString();//.ToString(BDU.UTIL.BDUConstants.);
                                            }
                                            catch (Exception ex)
                                            {
                                                rec.ffs.value = ""+rec.dvalue;
                                            }
                                        }
                                        else if (rec.ffs.fuid == UTIL.GlobalApp.Hotel_id_GUID_Billing_Status || rec.ffs.fuid == UTIL.GlobalApp.Hotel_id_GUID_Reservation_Status || rec.ffs.fuid == UTIL.GlobalApp.Hotel_id_GUID_CheckOut_Status || rec.ffs.fuid == UTIL.GlobalApp.Hotel_id_GUID_CheckIn_Status)
                                        {
                                            // API.GET_RESPCECTIVE_STATUS(SYNC_MODE.TO_PMS, rec.dvalue, pHotelId);
                                            rec.ffs.value = API.GET_RESPCECTIVE_STATUS(SYNC_MODE.TO_PMS, rec.dvalue, UTIL.GlobalApp.PMS_Version_No); // GlobalApp.GET_RESPCECTIVE_STATUS(API.LOVData,SYNC_MODE.TO_PMS, rec.dvalue);
                                        }
                                        else if (Convert.ToString(""+rec.dvalue).Contains(UTIL.BDUConstants.SPECIAL_KEYWORD_PREFIX))
                                            rec.ffs.value = string.Empty;
                                        else
                                            rec.ffs.value = ""+rec.dvalue;
                                    }
                                }//foreach (FormViewModel f in cmsData.forms)
                            }
                            cmsDatals.Add(dMapping);
                        }

                    }//  foreach (CoreDataEntity ets in retData)

                }//(data != null && data.jsonData!= null)

            }
            catch (Exception ex)
            {
                if (ex.ToString().ToLower().Contains(BDUConstants.API_UNATHENTICATED))
                {
                    UTIL.GlobalApp.JWT_Token = string.Empty;
                    UTIL.GlobalApp.Authentication_Done = false;
                }
            }
            return cmsDatals;
        }
        public static string StringValueOf(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            else
            {
                return value.ToString();
            }
        }
        public async Task<PMSVersionViewModel> getPMSVersion(int verionId)
        {
            List<PMSVersionServiceViewModel> pVersions = new List<PMSVersionServiceViewModel>();
            PMSVersionViewModel verModel = new PMSVersionViewModel();
            try
            {
                if (string.IsNullOrWhiteSpace(GlobalApp.JWT_Token) || !UTIL.GlobalApp.Authentication_Done) throw new Exception("Authorization Failed");

                //***********************Filters************************************//
                pms_data_filters filters = new pms_data_filters { id = verionId };

                //*********************** Service Client ************************************//
                var httpClient = new HttpClient();

                httpClient.BaseAddress = new Uri(GlobalApp.API_URI);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // JWT Token & call 
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + GlobalApp.JWT_Token);

                var inputContent = new StringContent(JsonSerializer.Serialize(filters), Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(GlobalApp.API_URI + API.POST_GET_PMS_VERSIONS, inputContent);
                response.EnsureSuccessStatusCode();
                var result = response.Content.ReadAsStringAsync();
                ServiceResponseViewModel resp = JsonSerializer.Deserialize<ServiceResponseViewModel>(result.Result, options);
                if (resp != null && !string.IsNullOrWhiteSpace(Convert.ToString(resp.jsonData)))
                {

                    pVersions = JsonSerializer.Deserialize<List<PMSVersionServiceViewModel>>(resp.jsonData.ToString(), options); //JsonSerializer.Deserialize<List<PMSVersionViewModel>>(resp.jsonData.ToString(), options);
                }
                if (pVersions != null && pVersions[0].jsonData.ToString().Length > 10)
                {
                    verModel.id = pVersions[0].id;
                    verModel.version = Convert.ToString(pVersions[0].id);
                    List<MappingViewModel> version = JsonSerializer.Deserialize<List<MappingViewModel>>(pVersions[0].jsonData.ToString(), options);
                    verModel.jsonData = version;
                }
                //else
                //    throw new Exception(string.Format("Failed to get pms versions's data."));

            }
            catch (Exception ex)
            {
                // pVersions = null;
                if (ex.ToString().ToLower().Contains(BDUConstants.API_UNATHENTICATED))
                {
                    UTIL.GlobalApp.JWT_Token = string.Empty;
                    UTIL.GlobalApp.Authentication_Done = false;
                }
                throw ex;
            }
            return verModel;
            //  return new HotelViewModel();
        }
        public async Task<List<PMSVersionsBindingViewModel>> getPMSVersions(int verionId = 0)
        {
            List<PMSVersionsBindingViewModel> pVersions = new List<PMSVersionsBindingViewModel>();
            try
            {
                if (string.IsNullOrWhiteSpace(GlobalApp.JWT_Token) || !UTIL.GlobalApp.Authentication_Done) throw new Exception("Authorization Failed");

                //***********************Filters************************************//
                pms_data_filters filters = new pms_data_filters { id = verionId };

                //*********************** Service Client ************************************//
                var httpClient = new HttpClient();

                httpClient.BaseAddress = new Uri(GlobalApp.API_URI);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // JWT Token & call 
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + GlobalApp.JWT_Token);

                var inputContent = new StringContent(JsonSerializer.Serialize(filters), Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(GlobalApp.API_URI + API.POST_GET_PMS_VERSIONS, inputContent);
                response.EnsureSuccessStatusCode();
                var result = response.Content.ReadAsStringAsync();
                ServiceResponseViewModel resp = JsonSerializer.Deserialize<ServiceResponseViewModel>(result.Result, options);
                if (resp != null && !string.IsNullOrWhiteSpace(Convert.ToString(resp.jsonData)))
                    pVersions = JsonSerializer.Deserialize<List<PMSVersionsBindingViewModel>>(Convert.ToString(resp.jsonData), options);
                //else
                //    throw new Exception(string.Format("Failed to get pms versions's data."));

            }
            catch (Exception ex)
            {
                // pVersions = null;
                if (ex.ToString().ToLower().Contains(BDUConstants.API_UNATHENTICATED))
                {
                    UTIL.GlobalApp.JWT_Token = string.Empty;
                    UTIL.GlobalApp.Authentication_Done = false;
                }
                throw ex;
            }
            return pVersions;
            //  return new HotelViewModel();
        }
        public async Task<List<LOVViewModel>> getLOVData()
        {
            List<LOVViewModel> statusLOVs = new List<LOVViewModel>();
            try
            {
                if (string.IsNullOrWhiteSpace(GlobalApp.JWT_Token) || !UTIL.GlobalApp.Authentication_Done) throw new Exception("Authorization Failed");

                //***********************Filters************************************//
                pms_lov_filters filters = new pms_lov_filters { table = "bdu_status_mappings", select = "pms_version_id,status,target_status" };

                //*********************** Service Client ************************************//
                var httpClient = new HttpClient();

                httpClient.BaseAddress = new Uri(GlobalApp.API_URI);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // JWT Token & call 
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + GlobalApp.JWT_Token);

                var inputContent = new StringContent(JsonSerializer.Serialize(filters), Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(GlobalApp.API_URI + API.POST_GET_LOV_DATA, inputContent);
                response.EnsureSuccessStatusCode();
                var result = response.Content.ReadAsStringAsync();
                ServiceResponseViewModel resp = JsonSerializer.Deserialize<ServiceResponseViewModel>(result.Result, options);
                if (resp != null && !string.IsNullOrWhiteSpace(Convert.ToString(resp.jsonData)))
                    statusLOVs = JsonSerializer.Deserialize<List<LOVViewModel>>(Convert.ToString(resp.jsonData), options);
                //else
                //    throw new Exception(string.Format("Failed to get pms versions's data."));

            }
            catch (Exception ex)
            {
                // pVersions = null;
                if (ex.ToString().ToLower().Contains(BDUConstants.API_UNATHENTICATED))
                {
                    UTIL.GlobalApp.JWT_Token = string.Empty;
                    UTIL.GlobalApp.Authentication_Done = false;
                }
                throw ex;
            }
            return statusLOVs;
            //  return new HotelViewModel();
        }
        public async Task<HotelViewModel> getHotelPresets(int pHotelid, string pHotelCode, int pSystemType, string pVersion)
        {
            HotelViewModel hotelPreset = null;
            try
            {
                if (string.IsNullOrWhiteSpace(GlobalApp.JWT_Token) || !UTIL.GlobalApp.Authentication_Done) throw new Exception("Authorization Failed");
                //  List<MappingViewModel> ls = new List<MappingViewModel>();
                var filterEntity = new Hotel_Filters { version = pVersion.ToString(), hotel_id = pHotelid, status = (int)UTIL.Enums.STATUSES.Active };
                var httpClient = new HttpClient();
                // serialize into json string
                httpClient.Timeout = TimeSpan.FromSeconds(40);
                httpClient.BaseAddress = new Uri(GlobalApp.API_URI);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // JWT Token & call 
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + GlobalApp.JWT_Token);
                // Serialize JSON before sending to API
                var jsonContent = new StringContent(JsonSerializer.Serialize(filterEntity, options), Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(GlobalApp.API_URI + API.POST_GET_HOTEL_PRESETS, jsonContent);
                response.EnsureSuccessStatusCode();
                var result = response.Content.ReadAsStringAsync();

                // serializeOptions.Converters.Add(System.Text.Json.Serialization.JsonConverter..Skip,JsonCommentHandling.Skip);
                if (("" + result.Result).Length > 10)
                {
                    List<ServiceResponseViewModel> resp = JsonSerializer.Deserialize<List<ServiceResponseViewModel>>(result.Result, options);
                    if (resp != null && resp.Count > 0 && !string.IsNullOrWhiteSpace(Convert.ToString(resp[0].jsonData)))
                    {
                        if (resp.Count > 0)
                        {
                            hotelPreset = JsonSerializer.Deserialize<HotelViewModel>(Convert.ToString(resp[0].jsonData), options);
                            hotelPreset.version = resp[0].version;
                            // hotelPreset.id = resp[0].id;
                        }
                        //else
                        //    hotelPreset = null;
                    }
                }
                //else
                //    throw new Exception(string.Format("Failed to get CMS data."));

            }
            catch (Exception ex)
            {
                hotelPreset = null;
                if (ex.ToString().ToLower().Contains(BDUConstants.API_UNATHENTICATED) || ex.ToString().ToLower().Contains(BDUConstants.API_UNAUTHORIZED))
                {
                    UTIL.GlobalApp.JWT_Token = string.Empty;
                    UTIL.GlobalApp.Authentication_Done = false;
                }
                throw new Exception(ex.Message);
            }
            return hotelPreset;
            //  return new HotelViewModel();
        }
        // pHotelId =0 default
        public async Task<UserViewModel> getUserDetails()
        {

            UserViewModel model = null;
            try
            {
                if (string.IsNullOrWhiteSpace(GlobalApp.JWT_Token) || !UTIL.GlobalApp.Authentication_Done) throw new Exception("Authorization Failed");
                //  List<MappingViewModel> ls = new List<MappingViewModel>();
                HttpClient httpClient = new HttpClient();
                // serialize into json string
                httpClient.BaseAddress = new Uri(GlobalApp.API_URI);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                // Set header before passing of request
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + GlobalApp.JWT_Token);
                var response = await httpClient.GetAsync(GlobalApp.API_URI + API.GET_USER_DETAILS);
                response.EnsureSuccessStatusCode();
                var result = response.Content.ReadAsStringAsync();
                model = JsonSerializer.Deserialize<UserViewModel>(result.Result, options);


            }
            catch (Exception ex)
            {
                if (ex.ToString().ToLower().Contains(BDUConstants.API_UNATHENTICATED))
                {
                    UTIL.GlobalApp.JWT_Token = string.Empty;
                    UTIL.GlobalApp.Authentication_Done = false;
                }
                model = null;
                throw ex;
            }
            return model;



        }
        public async Task<List<HotelViewModel>> getHotelslist(int pHotelId, int status)
        {
            // service. getHotelslist(int pHotelId, int status);
            List<HotelViewModel> hotelLs = new List<HotelViewModel>();
            ResponseViewModel resp = new ResponseViewModel();
            try
            {
                MappingViewModel mappingViewModel = new MappingViewModel();// { id = 0, entity_name = "Check In", status = 1, saves_status = 0, entity_type = (int)UTIL.Enums.ENTITY_TYPES.SYNC, mode = 2, pmsformid = "form", forms=new for, xpath = "/Pane[@Name=\u0022Desktop 1\u0022][@ClassName=\u0022#32769\u0022]/Window[@AutomationId=\u0022PMSForm\u0022][@Name=\u0022PMS Form\u0022]" };

                HttpClient httpClient = new HttpClient();
                // serialize into json string
                httpClient.BaseAddress = new Uri(GlobalApp.API_URI);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                // Set header before passing of request
                httpClient.Timeout = TimeSpan.FromSeconds(60);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Call API Method
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + GlobalApp.JWT_Token);
                var res = await httpClient.PostAsync($"{GlobalApp.API_URI}" + API.GET_HOTELS_LIST, null);
                res.EnsureSuccessStatusCode();
                var result = res.Content.ReadAsStringAsync();
                resp = JsonSerializer.Deserialize<ResponseViewModel>(result.Result, options);
                if (resp.status_code.ToString() == ((int)UTIL.Enums.ERROR_CODE.SUCCESS).ToString())
                    hotelLs = JsonSerializer.Deserialize<List<HotelViewModel>>(resp.jsonData.ToString(), options);
                else
                    throw new Exception("Failed to get hotels data from CMS.");

            }
            catch (Exception ex)
            {
                if (ex.ToString().ToLower().Contains(BDUConstants.API_UNATHENTICATED))
                {
                    UTIL.GlobalApp.JWT_Token = string.Empty;
                    UTIL.GlobalApp.Authentication_Done = false;
                }
                throw ex;
            }

            return hotelLs;

        }

        #endregion
        #region "Service Save Data & Log Out"

        public async Task<ResponseViewModel> saveAppLog(List<AppLogViewModel> logls, int hotel_id = 0) {
            ResponseViewModel resp = new ResponseViewModel();            
            try
            {  
                var httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(GlobalApp.API_URI);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // Serialize JSON before sending to API
                var logSon = new StringContent(JsonSerializer.Serialize(logls), Encoding.UTF8, "application/json");
                //Header  Authorization Headers
                httpClient.DefaultRequestHeaders.Add("email", BDUConstants.API_SPECIAL_AUTHENTICATION_USER);
                httpClient.DefaultRequestHeaders.Add("password", BDUConstants.API_SPECIAL_AUTHENTICATION_PWD);              
                var response = await httpClient.PostAsync(GlobalApp.API_URI + API.POST_SAVE_APP_LOG, logSon);
                response.EnsureSuccessStatusCode();
                var result = response.Content.ReadAsStringAsync();
                resp = JsonSerializer.Deserialize<ResponseViewModel>(result.Result);
                resp.jsonData = null;
                resp.status = true;
                resp.status_code = ((int)UTIL.Enums.ERROR_CODE.SUCCESS).ToString();
            }
            catch (Exception ex)
            {                
                resp.status = false;
                resp.status_code = ((int)UTIL.Enums.ERROR_CODE.FAILED).ToString();
                throw ex;
            }
            return resp;

        }
        public async Task<ResponseViewModel> saveHotelPresets(int pHotelid, string pHotelname, HotelViewModel preset, string pVersion)
        {
            ResponseViewModel resp = new ResponseViewModel();
            PresetViewModel presets = new PresetViewModel();
            try
            {
                // Check APi Status
                if (string.IsNullOrWhiteSpace(GlobalApp.JWT_Token) || !UTIL.GlobalApp.Authentication_Done) throw new Exception("Already logged out, there is no login session");
                // ZERO for new
                presets.id = preset.id==0? Convert.ToInt32(pVersion): preset.id;
                presets.time = GlobalApp.CurrentDateTime.AddSeconds(GlobalApp.DifferenceinSecs);
                presets.created_by = GlobalApp.User_id;
                presets.updated_by = GlobalApp.User_id;
                presets.hotel_id = pHotelid;
                preset.version = Regex.IsMatch(pVersion, @"^\d+$") ? pVersion : "0";
                presets.version = Regex.IsMatch(pVersion, @"^\d+$") ? pVersion : "0";
                presets.jsonData = preset;
                presets.Status = (int)UTIL.Enums.STATUSES.Active;


                var httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(GlobalApp.API_URI);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // Serialize JSON before sending to API
                var jsonContent = new StringContent(JsonSerializer.Serialize(presets, options), Encoding.UTF8, "application/json");

                //Header  Authorization for JWT Token
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + GlobalApp.JWT_Token);
                // Call Logout
                var response = await httpClient.PostAsync(GlobalApp.API_URI + API.POST_SAVE_PMS_PRESETS, jsonContent);
                response.EnsureSuccessStatusCode();
                var result = response.Content.ReadAsStringAsync();
                if (!string.IsNullOrWhiteSpace(result.Result))
                {
                    ServiceResponseViewModel sresp = JsonSerializer.Deserialize<ServiceResponseViewModel>(result.Result);
                    if (sresp.status_code.ToString() == ((int)UTIL.Enums.ERROR_CODE.SUCCESS).ToString())
                    {
                        resp.jsonData = sresp.jsonData;
                        resp.status = true;
                        // resp.status_code = ((int)UTIL.Enums.ERROR_CODE.SUCCESS).ToString();
                    }

                }
            }
            catch (Exception ex)
            {
                if (ex.ToString().ToLower().Contains(BDUConstants.API_UNATHENTICATED))
                {
                    UTIL.GlobalApp.JWT_Token = string.Empty;
                    UTIL.GlobalApp.Authentication_Done = false;
                }
                resp.status = false;
                resp.message = ex.InnerException.ToString();
                resp.status_code = ((int)UTIL.Enums.ERROR_CODE.FAILED).ToString();
                throw ex;
            }
            return resp;
            // return new ResponseViewModel();
        }
        public async Task<ResponseViewModel> savePMSVersion(List<MappingViewModel> mappings, int id = 0, int status = 1, string version = "V1")
        {
            ResponseViewModel resp = new ResponseViewModel();
            PMSVersionViewModel pmsVersion = new PMSVersionViewModel();
            try
            {
                pmsVersion.id = id;
                pmsVersion.version = version;
                pmsVersion.Status = status;
                pmsVersion.jsonData = mappings;
                // Check APi Status
                if (string.IsNullOrWhiteSpace(GlobalApp.JWT_Token) || !UTIL.GlobalApp.Authentication_Done) throw new Exception("Authentication failed, required access token");

                var httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(GlobalApp.API_URI);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // Serialize JSON before sending to API
                var jsonContent = new StringContent(JsonSerializer.Serialize(pmsVersion), Encoding.UTF8, "application/json");
                //Header  Authorization for JWT Token
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + GlobalApp.JWT_Token);
                // Call Logout
                var response = await httpClient.PostAsync(GlobalApp.API_URI + API.POST_SAVE_PMS_VERSION, jsonContent);
                response.EnsureSuccessStatusCode();
                var result = response.Content.ReadAsStringAsync();
                resp = JsonSerializer.Deserialize<ResponseViewModel>(result.Result);
                resp.jsonData = null;
                resp.status = true;
                resp.status_code = ((int)UTIL.Enums.ERROR_CODE.SUCCESS).ToString();
            }
            catch (Exception ex)
            {
                if (ex.ToString().ToLower().Contains(BDUConstants.API_UNATHENTICATED))
                {
                    UTIL.GlobalApp.JWT_Token = string.Empty;
                    UTIL.GlobalApp.Authentication_Done = false;
                }
                resp.status = false;
                resp.status_code = ((int)UTIL.Enums.ERROR_CODE.FAILED).ToString();
                throw ex;
            }
            return resp;
        }
        public async Task<ResponseViewModel> saveSettingsData( ConfigurationAndSettingsViewModel model, int hotel_id = 0)//, int id = 0, int status = 1, string version = "V1")
        {
            ResponseViewModel resp = new ResponseViewModel();
            PMSVersionViewModel pmsVersion = new PMSVersionViewModel();
            try
            {
                model.hotel_id = hotel_id;
                model.description = "Update at" + System.DateTime.Now.ToString();
                model.root_path = string.IsNullOrWhiteSpace(model.root_path) ? "." : model.root_path;
                if (string.IsNullOrWhiteSpace(model.hotel_code) || (""+model.hotel_code).Length<=1) throw new Exception("Hotel Code is required, save can't be proceeded without hotel_code");
                // Check APi Status
                if (string.IsNullOrWhiteSpace(GlobalApp.JWT_Token) || !UTIL.GlobalApp.Authentication_Done) throw new Exception("Authentication failed, required access token");

                var httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(GlobalApp.API_URI);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // Serialize JSON before sending to API
                var jsonContent = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
                //Header  Authorization for JWT Token
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + GlobalApp.JWT_Token);
                // Call Logout
                var response = await httpClient.PostAsync(GlobalApp.API_URI + API.POST_SAVE_SETTINGS, jsonContent);
                response.EnsureSuccessStatusCode();
                var result = response.Content.ReadAsStringAsync();
                resp = JsonSerializer.Deserialize<ResponseViewModel>(result.Result);
                resp.jsonData = null;
                resp.status = true;
                resp.status_code = ((int)UTIL.Enums.ERROR_CODE.SUCCESS).ToString();
            }
            catch (Exception ex)
            {
                if (ex.ToString().ToLower().Contains(BDUConstants.API_UNATHENTICATED))
                {
                    UTIL.GlobalApp.JWT_Token = string.Empty;
                    UTIL.GlobalApp.Authentication_Done = false;
                }
                resp.status = false;
                resp.status_code = ((int)UTIL.Enums.ERROR_CODE.FAILED).ToString();
                throw ex;
            }
            return resp;
        }

        public async Task<MappingBindingViewModel> saveEntityField(MappingBindingViewModel model)
        {
            ResponseViewModel resp = new ResponseViewModel();
            EntityFieldViewModel field = new EntityFieldViewModel();
            try
            {
                // Check APi Status
                if (string.IsNullOrWhiteSpace(GlobalApp.JWT_Token) || !UTIL.GlobalApp.Authentication_Done) throw new Exception("Already logged out, there is no login session");

                field.field_desc = model.field_desc;
                field.entity_id = model.entity_id;
                field.sr = model.fieldsr;
                field.parent_field_id = string.Empty;
                field.pms_field_xpath = model.pms_field_xpath;
                field.pms_field_expression = model.pms_field_expression;
                field.is_unmapped = string.IsNullOrWhiteSpace(model.schema_field_name) ? 1 : 0;
                field.data_type = model.data_type;
                field.default_value = model.default_value;
                field.status = model.fieldstatus;
                field.format = model.fieldformat;
                field.mandatory = model.fieldmanadatory;
                field.is_reference = model.is_reference;
                field.fuid = model.fuid;
                field.parent_field_id = model.parent_field_id;

                var httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(GlobalApp.API_URI);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // Serialize JSON before sending to API
                var jsonContent = new StringContent(JsonSerializer.Serialize(field), Encoding.UTF8, "application/json");

                //Header  Authorization for JWT Token
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + GlobalApp.JWT_Token);
                // Call Logout
                var response = await httpClient.PostAsync(GlobalApp.API_URI + API.POST_SAVE_ENTITY_FIELD, jsonContent);
                response.EnsureSuccessStatusCode();
                var result = response.Content.ReadAsStringAsync();
                // resp = JsonSerializer.Deserialize<ResponseViewModel>(result.Result);
                resp = JsonSerializer.Deserialize<ResponseViewModel>(result.Result, options);
                if (resp != null && resp.status_code.ToString() == ((int)UTIL.Enums.ERROR_CODE.SUCCESS).ToString())
                    field = JsonSerializer.Deserialize<EntityFieldViewModel>(Convert.ToString(resp.jsonData), options);
                else
                    throw new Exception(string.Format("Failed to get CMS data."));
                resp.jsonData = null;
                resp.status = true;
                resp.status_code = ((int)UTIL.Enums.ERROR_CODE.SUCCESS).ToString();
                model.fieldId = field.id;
            }
            catch (Exception ex)
            {
                if (ex.ToString().ToLower().Contains(BDUConstants.API_UNATHENTICATED))
                {
                    UTIL.GlobalApp.JWT_Token = string.Empty;
                    UTIL.GlobalApp.Authentication_Done = false;
                }
                resp.status = true;
                resp.status_code = ((int)UTIL.Enums.ERROR_CODE.FAILED).ToString();
                // model.fieldId = model.fieldsr;
                throw ex;
                // return model;
            }
            return model;
        }

        public async Task<ResponseViewModel> saveCMSData(int hotel_id, List<MappingViewModel> mCollection)
        {
            ResponseViewModel resp = new ResponseViewModel();
            bool undoCalled = false;
            try
            {
                EntityData eData = new EntityData { hotel_id = hotel_id, time = GlobalApp.CurrentDateTime.AddSeconds(GlobalApp.DifferenceinSecs).ToString("yyyy-MM-dd HH:mm:ss") };
                List<CoreDataEntity> dEntyCollection = new List<CoreDataEntity>();
                var saveCollections = mCollection.Where(x => x.entity_type == (int)UTIL.Enums.ENTITY_TYPES.SYNC).OrderBy(t => t.entity_Id).ToList();
                // Prepare Data
                foreach (MappingViewModel pmsData in saveCollections)
                {
                    CoreDataEntity entity = new CoreDataEntity();
                    if (pmsData.undo >= 1)
                        undoCalled = true;
                    // MappingViewModel cmsData = mCollection.Where(x => x.entity_Id == ets.entity_id).FirstOrDefault().Clone();
                    List<CoreDataEntityRecords> cRecords = new List<CoreDataEntityRecords>();
                    CoreDataEntityRecords cRecord = new CoreDataEntityRecords();
                    List<CoreFieldData> fields = new List<CoreFieldData>();
                    entity.entity_id = pmsData.entity_Id;
                    pmsData.roomno= string.IsNullOrWhiteSpace(pmsData.roomno)? string.IsNullOrWhiteSpace(pmsData.forms.FirstOrDefault().fields.Where(x => x.field_desc.ToLower().Contains("room number")).FirstOrDefault() == null ? "" : pmsData.forms.FirstOrDefault().fields.Where(x => x.field_desc.ToLower().Contains("room number")).FirstOrDefault().value) ? pmsData.forms.FirstOrDefault().fields.Where(x => x.field_desc.ToLower().Contains("rmno")).FirstOrDefault() == null ? "" : pmsData.forms.FirstOrDefault().fields.Where(x => x.field_desc.ToLower().Contains("rmno")).FirstOrDefault().value : pmsData.forms.FirstOrDefault().fields.Where(x => x.field_desc.ToLower().Contains("room number")).FirstOrDefault() == null ? "" : pmsData.forms.FirstOrDefault().fields.Where(x => x.field_desc.ToLower().Contains("room number")).FirstOrDefault().value: pmsData.roomno;
                    foreach (FormViewModel f in pmsData.forms)
                    {
                        
                        foreach (EntityFieldViewModel flds in f.fields)
                        {
                            if (flds.fuid == UTIL.GlobalApp.Hotel_id_GUID )//&& pmsData.entity_Id == (int)UTIL.Enums.ENTITIES.CHECKIN)
                            {
                                flds.value = hotel_id.ToString();
                            }
                            else if (flds.is_reference == 1 && string.IsNullOrWhiteSpace(cRecord.id))
                            {
                                //cRecord.id = (flds.is_reference == 1) ? flds.value : flds.default_value;
                                cRecord.id = (flds.is_reference == 1) ? pmsData.reference : flds.value;
                                cRecord.fuid = flds.fuid;
                                cRecord.roomno = pmsData.roomno;
                                string fValue = string.IsNullOrWhiteSpace(flds.value) && flds.mandatory == 1 ? "" + flds.default_value : "" + flds.value;
                                if (flds.data_type == (int)UTIL.Enums.DATA_TYPES.DATE && !string.IsNullOrWhiteSpace(fValue))
                                    fields.Add(new CoreFieldData { fuid = flds.fuid, value = System.DateTime.Now.ToString("yyyy-MM-dd") });
                                else if (flds.data_type == (int)UTIL.Enums.DATA_TYPES.DATETIME && !string.IsNullOrWhiteSpace(fValue))
                                    fields.Add(new CoreFieldData { fuid = flds.fuid, value = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") });
                                else if (Convert.ToString(fValue).Contains(UTIL.BDUConstants.ROOM_NO_KEYWORD) || Convert.ToString(fValue).Contains(UTIL.BDUConstants.GUEST_NAME_KEYWORD))
                                {
                                    fields.Add(new CoreFieldData { fuid = flds.fuid, value = "" });
                                }
                                else
                                    fields.Add(new CoreFieldData { fuid = flds.fuid, value = fValue });
                            }
                             else if (UTIL.GlobalApp.Hotel_id_GUID_Billing_Status == flds.fuid || UTIL.GlobalApp.Hotel_id_GUID_CheckOut_Status == flds.fuid || UTIL.GlobalApp.Hotel_id_GUID_Reservation_Status == flds.fuid || UTIL.GlobalApp.Hotel_id_GUID_CheckIn_Status == flds.fuid)
                           {
                                string fValue = string.IsNullOrWhiteSpace(flds.value) && flds.mandatory == 1 ? "" + flds.default_value : "" + flds.value;
                                fields.Add(new CoreFieldData { fuid = flds.fuid, value = API.GET_RESPCECTIVE_STATUS(SYNC_MODE.TO_CMS, fValue, UTIL.GlobalApp.PMS_Version_No, f.entityid) });// BDU.UTIL.GlobalApp.GET_RESPCECTIVE_STATUS(SYNC_MODE.TO_CMS, fValue) });
                            }
                            else
                            {
                                string fValue = string.IsNullOrWhiteSpace(flds.value) && flds.mandatory == 1 ? "" + flds.default_value : "" + flds.value;
                                if (flds.data_type == (int)UTIL.Enums.DATA_TYPES.DATE && !string.IsNullOrWhiteSpace(fValue))
                                    fields.Add(new CoreFieldData { fuid = flds.fuid, value = DateTime.Parse(fValue.ToString()).ToString("yyyy-MM-dd") });
                                else if (flds.data_type == (int)UTIL.Enums.DATA_TYPES.DATETIME && !string.IsNullOrWhiteSpace(fValue))
                                    fields.Add(new CoreFieldData { fuid = flds.fuid, value = DateTime.Parse(fValue.ToString()).ToString("yyyy-MM-dd HH:mm:ss") });  // System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") });
                                else if (Convert.ToString(fValue).Contains(UTIL.BDUConstants.ROOM_NO_KEYWORD) || Convert.ToString(fValue).Contains(UTIL.BDUConstants.GUEST_NAME_KEYWORD))
                                {
                                    fields.Add(new CoreFieldData { fuid = flds.fuid, value = "" });
                                }
                                else
                                    fields.Add(new CoreFieldData { fuid = flds.fuid, value = fValue });
                            }
                        }
                    }
                    // Hotel Entity Record made Default
                    if (pmsData.entity_Id == (int)UTIL.Enums.ENTITIES.CHECKIN && !fields.Where(x => x.fuid == UTIL.GlobalApp.Hotel_id_GUID).Any())//FirstOrDefault()==null )
                    {
                        fields.Add(new CoreFieldData { fuid = UTIL.GlobalApp.Hotel_id_GUID, value = hotel_id.ToString() });
                    }
                    cRecord.data = fields;
                    cRecords.Add(cRecord);
                    entity.data = cRecords;
                    dEntyCollection.Add(entity);
                }//entities
                // Check APi Status
                if (string.IsNullOrWhiteSpace(GlobalApp.JWT_Token) || !UTIL.GlobalApp.Authentication_Done) throw new Exception("Already logged out, there is no login session");
                // ZERO for new
                Save_CMS_Data_Filters sFilter = new Save_CMS_Data_Filters { hotel_id = hotel_id, time = GlobalApp.CurrentDateTime.AddSeconds(GlobalApp.DifferenceinSecs), created_at = GlobalApp.CurrentDateTime.AddSeconds(GlobalApp.DifferenceinSecs), created_by = GlobalApp.User_id, jsonData = dEntyCollection };

                var httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(GlobalApp.API_URI);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // Serialize JSON before sending to API
                var jsonContent = new StringContent(JsonSerializer.Serialize(sFilter), Encoding.UTF8, "application/json");

                //Header  Authorization for JWT Token
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + GlobalApp.JWT_Token);
                // Call Logout
                string postURL = GlobalApp.API_URI + API.POST_SAVE_CMS_DATA;
                if (undoCalled)
                    postURL = GlobalApp.API_URI + API.POST_REVERSE_CHECKIN_CHECKOUT;              
                var response = await httpClient.PostAsync(postURL, jsonContent);
                response.EnsureSuccessStatusCode();
                var result = response.Content.ReadAsStringAsync();
                resp = JsonSerializer.Deserialize<ResponseViewModel>(result.Result);
                resp.jsonData = null;
                resp.status = true;
                resp.status_code = ((int)UTIL.Enums.ERROR_CODE.SUCCESS).ToString();
            }
            catch (Exception ex)
            {
                if (ex.ToString().ToLower().Contains(BDUConstants.API_UNATHENTICATED))
                {
                    UTIL.GlobalApp.JWT_Token = string.Empty;
                    UTIL.GlobalApp.Authentication_Done = false;
                }
                resp.status = false;
                resp.status_code = ((int)UTIL.Enums.ERROR_CODE.FAILED).ToString();
                throw ex;
            }
            return resp;
        }

        public async Task<ResponseViewModel> logOut()
        {
            ResponseViewModel responseViewModel = new ResponseViewModel();
            try
            {
                if (string.IsNullOrWhiteSpace(GlobalApp.JWT_Token) || !UTIL.GlobalApp.Authentication_Done) throw new Exception("Already logged out, there is no login session");
                var httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(GlobalApp.API_URI);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Header  Authorization for JWT Token
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + GlobalApp.JWT_Token);
                // Call Logout
                var response = await httpClient.PostAsync(GlobalApp.API_URI + API.POST_LOGOUT, null);
                response.EnsureSuccessStatusCode();
                var result = response.Content.ReadAsStringAsync();
                responseViewModel = JsonSerializer.Deserialize<ResponseViewModel>(result.Result);
                responseViewModel.jsonData = null;
                responseViewModel.status = true;
                responseViewModel.status_code = ((int)UTIL.Enums.ERROR_CODE.SUCCESS).ToString();
            }
            catch (Exception ex)
            {
                UTIL.GlobalApp.JWT_Token = string.Empty;
                UTIL.GlobalApp.Authentication_Done = false;
                responseViewModel.status = false;
                responseViewModel.status_code = ((int)UTIL.Enums.ERROR_CODE.FAILED).ToString();
                throw ex;
            }
            return responseViewModel;
        }
        //public async Task<ResponseViewModel> logOut(string pEmail, int pUserid)
        //{
        //    List<MappingViewModel> ls = new List<MappingViewModel>();
        //    //var listByName = await _addressAppService.GetAddressList(name);
        //    //var mappedByName = _mapper.Map<IEnumerable<AddressViewModel>>(listByName);
        //    return new ResponseViewModel();
        //}

        #endregion
    }
}
