using System;
using System.Collections.Generic;
using TypingApp.Commands;
using TypingApp.Services;

namespace TypingApp.Models
{
    public class Group
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public string GroupCode { get; set; }

        public Group(int groupId, string groupName, string groupCode)
        {
            GroupId = groupId;
            GroupName = groupName;
            GroupCode = groupCode;
        }

        public Group(IReadOnlyDictionary<string, object> props)
        {
            GroupId = (int)props["id"];
            GroupName = (string)props["name"];
            GroupCode = (string)props["code"];
        }

        public void RefreshCode()
        {
            GroupCode = new GroupCodeService().GenerateCode();
        }
    }
}