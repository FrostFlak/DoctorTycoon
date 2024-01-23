using System;
using UnityEngine;

namespace UI
{
    public class FormatNumsHelper
    {
        private string[] names = new[]
        {
            "",
            "K",
            "M",
            "B",
            "T"
        };
    
        public string FormatNum(float num)
        {
            if (num == 0) return "0";

            num = Mathf.Round(num);

            int i = 0;
            while (i + 1 < names.Length && num >= 1000f)
            {
                num /= 1000f;
                i++;
            }

            return num.ToString("#.##") + names[i];
        }
    }
}
