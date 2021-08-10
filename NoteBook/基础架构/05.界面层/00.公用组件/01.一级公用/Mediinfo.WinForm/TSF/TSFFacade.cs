using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Mediinfo.WinForm
{
    public class TSFFacade
    {
        public static short[] GetLangIDs()
        {
            List<short> langIDs = new List<short>();
            ITfInputProcessorProfiles profiles;
            if (TSF_NativeAPI.TF_CreateInputProcessorProfiles(out profiles) == 0)
            {
                IntPtr langPtrs;
                int fetchCount = 0;
                if (profiles.GetLanguageList(out langPtrs, out fetchCount) == 0)
                {
                    for (int i = 0; i < fetchCount; i++)
                    {
                        short id = Marshal.ReadInt16(langPtrs, sizeof(short) * i);
                        langIDs.Add(id);
                    }
                }
                Marshal.ReleaseComObject(profiles);
            }
            return langIDs.ToArray();
        }

        public static bool ActiveInputMethodWithDesc(short langID, string desc)
        {
            ITfInputProcessorProfiles profiles;
            if (TSF_NativeAPI.TF_CreateInputProcessorProfiles(out profiles) == 0)
            {
                try
                {
                    IEnumTfLanguageProfiles enumerator = null;
                    if (profiles.EnumLanguageProfiles(langID, out enumerator) == 0)
                    {
                        if (enumerator != null)
                        {
                            TF_LANGUAGEPROFILE[] langProfile = new TF_LANGUAGEPROFILE[1];
                            int fetchCount = 0;
                            while (enumerator.Next(1, langProfile, out fetchCount) == 0)
                            {
                                IntPtr ptr;
                                if (profiles.GetLanguageProfileDescription(ref langProfile[0].clsid, langProfile[0].langid, ref langProfile[0].guidProfile, out ptr) == 0)
                                {
                                    bool enabled;
                                    if (profiles.IsEnabledLanguageProfile(ref langProfile[0].clsid, langProfile[0].langid, ref langProfile[0].guidProfile, out enabled) == 0)
                                    {
                                        if (enabled)
                                        {
                                            string s = Marshal.PtrToStringBSTR(ptr);
                                            if (s.Equals(desc))
                                            {
                                               
                                                if (profiles.ChangeCurrentLanguage(langProfile[0].langid)==0)
                                                {
                                                    return profiles.ActivateLanguageProfile(ref langProfile[0].clsid, langProfile[0].langid, ref langProfile[0].guidProfile) == 0;
                                                }
                                                
                                            }
                                               
                                        }
                                    }
                                    Marshal.FreeBSTR(ptr);
                                }
                            }
                        }
                    }
                }
                finally
                {
                    Marshal.ReleaseComObject(profiles);
                }
            }
            return false;
        }

        public static string[] GetInputMethodList(short langID)
        {
            List<string> imeList = new List<string>();
            ITfInputProcessorProfiles profiles;
            if (TSF_NativeAPI.TF_CreateInputProcessorProfiles(out profiles) == 0)
            {
                try
                {
                    IEnumTfLanguageProfiles enumerator = null;
                    if (profiles.EnumLanguageProfiles(langID, out enumerator) == 0)
                    {
                        if (enumerator != null)
                        {
                            TF_LANGUAGEPROFILE[] langProfile = new TF_LANGUAGEPROFILE[1];
                            int fetchCount = 0;
                            while (enumerator.Next(1, langProfile, out fetchCount) == 0)
                            {
                                IntPtr ptr;
                                if (profiles.GetLanguageProfileDescription(ref langProfile[0].clsid, langProfile[0].langid, ref langProfile[0].guidProfile, out ptr) == 0)
                                {
                                    bool enabled;
                                    if (profiles.IsEnabledLanguageProfile(ref langProfile[0].clsid, langProfile[0].langid, ref langProfile[0].guidProfile, out enabled) == 0)
                                    {
                                        if (enabled)
                                            imeList.Add(Marshal.PtrToStringBSTR(ptr));
                                    }
                                }
                                Marshal.FreeBSTR(ptr);
                            }
                        }
                    }
                }
                finally
                {
                    Marshal.ReleaseComObject(profiles);
                }
            }
            return imeList.ToArray();
        }

        public static string GetCurrentInputMethodDesc(short langID)
        {
            ITfInputProcessorProfiles profiles;
            if (TSF_NativeAPI.TF_CreateInputProcessorProfiles(out profiles) == 0)
            {
                try
                {
                    Guid clsid;
                    Guid profileid;
                    Guid catid = new Guid(TSF_NativeAPI.GUID_TFCAT_TIP_KEYBOARD.ToByteArray());
                    if (profiles.GetDefaultLanguageProfile(langID, ref catid, out clsid, out profileid) == 0)
                    {
                        if (profiles.GetActiveLanguageProfile(ref clsid, out langID, out profileid) == 0)
                        {
                            IntPtr ptr;
                            try
                            {
                                if (profiles.GetLanguageProfileDescription(ref clsid, langID, ref profileid, out ptr) == 0)
                                {
                                    string s = Marshal.PtrToStringBSTR(ptr);
                                    Marshal.FreeBSTR(ptr);
                                    return s;
                                }
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }
                }
                finally
                {
                    Marshal.ReleaseComObject(profiles);
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// 关闭当前输入法
        /// </summary>
        /// <returns></returns>
        public static bool DeActiveInputMethod()
        {
            ITfInputProcessorProfiles profiles;
            if (TSF_NativeAPI.TF_CreateInputProcessorProfiles(out profiles) == 0)
            {
                try
                {
                    short langID;

                    profiles.GetCurrentLanguage(out langID);
                    if (TSF_NativeAPI.TF_CreateInputProcessorProfiles(out profiles) == 0)
                    {
                        Guid clsid = Guid.Empty;
                        return profiles.ActivateLanguageProfile(ref clsid, langID, ref clsid) == 0;
                    }
                    return false;
                }
                finally
                {
                    Marshal.ReleaseComObject(profiles);
                }
            }
            return false;
        }
    }
}