using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using CS_Emerios_API_Tracker.Helper;

namespace CS_Emerios_API_Tracker.Core.DTOs
{
    public class UserActiveDirectoryDTO
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("userprincipalname")]
        public string UserPrincipalName { get; set; }

        [JsonPropertyName("lockouttime")]
        public string LockoutTime { get; set; }

        [JsonPropertyName("givenname")]
        public string GivenName { get; set; }

        [JsonPropertyName("whencreated")]
        public string WhenCreated { get; set; }

        [JsonPropertyName("whenchanged")]
        public string WhenChanged { get; set; }

        [JsonPropertyName("badpasswordtime")]
        public string BadPasswordTime { get; set; }

        [JsonPropertyName("lastlogontimestamp")]
        public string LastLogonTimestamp { get; set; }

        [JsonPropertyName("cn")]
        public string Cn { get; set; }

        [JsonPropertyName("logoncount")]
        public int LogonCount { get; set; }

        [JsonPropertyName("adspath")]
        public string AdsPath { get; set; }

        [JsonPropertyName("objectclass")]
        public string objectclass { get; set; }

        [JsonPropertyName("msds-supportedencryptiontypes")]
        public int MsdsSupportedEncryptionTypes { get; set; }

        [JsonPropertyName("samaccounttype")]
        public int SamAccountType { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("primarygroupid")]
        public int PrimaryGroupId { get; set; }

        [JsonPropertyName("lastlogoff")]
        public string LastLogoff { get; set; }

        [JsonPropertyName("memberof")]
        public string MemberOf { get; set; }

        [JsonPropertyName("objectsid")]
        public string ObjectSid { get; set; }

        [JsonPropertyName("c")]
        public string C { get; set; }

        [JsonPropertyName("objectguid")]
        public string ObjectGuid { get; set; }

        [JsonPropertyName("pwdlastset")]
        public string PwdLastSet { get; set; }

        [JsonPropertyName("accountexpires")]
        public string AccountExpires { get; set; }

        [JsonPropertyName("usercertificate")]
        public string UserCertificate { get; set; }

        [JsonPropertyName("displayname")]
        public string DisplayName { get; set; }

        [JsonPropertyName("usncreated")]
        public string UsnCreated { get; set; }

        [JsonPropertyName("mail")]
        public string Mail { get; set; }

        [JsonPropertyName("dscorepropagationdata")]
        public string DScorePropagationData { get; set; }

        [JsonPropertyName("admincount")]
        public int AdminCount { get; set; }

        [JsonPropertyName("sn")]
        public string Sn { get; set; }

        [JsonPropertyName("department")]
        public string Department { get; set; }

        [JsonPropertyName("initials")]
        public string Initials { get; set; }

        [JsonPropertyName("samaccountname")]
        public string SamAccountName { get; set; }

        [JsonPropertyName("lastlogon")]
        public string LastLogon { get; set; }

        [JsonPropertyName("countrycode")]
        public int CountryCode { get; set; }

        [JsonPropertyName("co")]
        public string Co { get; set; }

        [JsonPropertyName("badpwdcount")]
        public int BadPwdCount { get; set; }

        [JsonPropertyName("useraccountcontrol")]
        public int UserAccountControl { get; set; }

        [JsonPropertyName("objectcategory")]
        public string ObjectCategory { get; set; }

        [JsonPropertyName("distinguishedname")]
        public string DistinguishedName { get; set; }

        [JsonPropertyName("manager")]
        public string Manager { get; set; }

        [JsonPropertyName("usnchanged")]
        public string UsnChanged { get; set; }

        [JsonPropertyName("instancetype")]
        public int InstanceType { get; set; }
    }
}
