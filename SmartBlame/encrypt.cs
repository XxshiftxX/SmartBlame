using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace SmartBlame
{

    public class Encryptor
    {
        enum EnumCho
        { ㄱ, ㄲ, ㄴ, ㄷ, ㄸ, ㄹ, ㅁ, ㅂ, ㅃ, ㅅ, ㅆ, ㅇ, ㅈ, ㅉ, ㅊ, ㅋ, ㅌ, ㅍ, ㅎ }
        enum EnumJung
        { ㅏ, ㅐ, ㅑ, ㅒ, ㅓ, ㅔ, ㅕ, ㅖ, ㅗ, ㅘ, ㅙ, ㅚ, ㅛ, ㅜ, ㅝ, ㅞ, ㅟ, ㅠ, ㅡ, ㅢ, ㅣ }
        enum EnumJong
        { x, ㄱ, ㄲ, ㄳ, ㄴ, ㄵ, ㄶ, ㄷ, ㄹ, ㄺ, ㄻ, ㄼ, ㄽ, ㄾ, ㄿ, ㅀ, ㅁ, ㅂ, ㅄ, ㅅ, ㅆ, ㅇ, ㅈ, ㅊ, ㅋ, ㅌ, ㅍ, ㅎ}

        bool PairConsonant = false;
        bool RemoveIeung = true;
        bool Yamin = true;
        bool AdvancedChange = false;

        char[] __Cho = { 'ㄱ', 'ㄲ', 'ㄴ', 'ㄷ', 'ㄸ', 'ㄹ', 'ㅁ', 'ㅂ', 'ㅃ', 'ㅅ', 'ㅆ', 'ㅇ', 'ㅈ', 'ㅉ',
                'ㅊ', 'ㅋ', 'ㅌ', 'ㅍ', 'ㅎ'};
        char[] __Jung = { 'ㅏ', 'ㅐ', 'ㅑ', 'ㅒ', 'ㅓ', 'ㅔ', 'ㅕ', 'ㅖ', 'ㅗ', 'ㅘ', 'ㅙ', 'ㅚ', 'ㅛ', 'ㅜ',
                'ㅝ', 'ㅞ', 'ㅟ', 'ㅠ', 'ㅡ', 'ㅢ', 'ㅣ' };
        char[] __Jong = { ' ', 'ㄱ', 'ㄲ', 'ㄳ', 'ㄴ', 'ㄵ', 'ㄶ', 'ㄷ', 'ㄹ', 'ㄺ', 'ㄻ', 'ㄼ', 'ㄽ', 'ㄾ',
                    'ㄿ', 'ㅀ', 'ㅁ', 'ㅂ', 'ㅄ', 'ㅅ', 'ㅆ', 'ㅇ', 'ㅈ', 'ㅊ', 'ㅋ', 'ㅌ',
                    'ㅍ', 'ㅎ'};

        int[] analizeCho;
        int[] analizeJung;
        int[] analizeJong;
        bool[] analizeHangeul;

        private void Analize(string input)
        {
            analizeCho = new int[input.Length];
            analizeJung = new int[input.Length];
            analizeJong = new int[input.Length];
            analizeHangeul = new bool[input.Length];

            int loop = 0;
            foreach(char tempChar in input)
            {
                if(char.GetUnicodeCategory(tempChar) == System.Globalization.UnicodeCategory.OtherLetter)
                {
                    int temp = tempChar - 0xAC00;
                    
                    analizeJong[loop] = temp % 28;
                    analizeJung[loop] = ((temp - analizeJong[loop]) / 28) % 21;
                    analizeCho[loop] = (((temp - analizeJong[loop]) / 28) - analizeJung[loop]) / 21;
                    analizeHangeul[loop] = true;
                }
                else
                {
                    analizeCho[loop] = tempChar;
                    analizeHangeul[loop] = false;
                }
                loop++;
            }
        }

        public string Encrypt(string input)
        {
            string result = string.Empty;

            Analize(input);

            if(RemoveIeung)
            {
                for(int i = 0; i < analizeHangeul.Length-1; i++)
                {
                    if(analizeCho[i+1] == (int)EnumCho.ㅇ && analizeJong[i] != (int)EnumJong.x)
                    {
                        analizeCho[i + 1] = Array.IndexOf(__Cho, __Jong[analizeJong[i]]);
                        analizeJong[i] = (int)EnumJong.x;
                    }
                }
            }

            // 야민정음
            if (Yamin)
            {
                for (int i = 0; i < analizeHangeul.Length; i++)
                {
                    // 대 > 머
                    if (analizeCho[i] == (int)EnumCho.ㄷ && analizeJung[i] == (int)EnumJung.ㅐ)
                    {
                        analizeCho[i] = (int)EnumCho.ㅁ;
                        analizeJung[i] = (int)EnumJung.ㅓ;
                    }
                    // 과 > 파
                    else if (analizeCho[i] == (int)EnumCho.ㄱ && analizeJung[i] == (int)EnumJung.ㅘ)
                    {
                        analizeCho[i] = (int)EnumCho.ㅍ;
                        analizeJung[i] = (int)EnumJung.ㅏ;
                    }
                    // 고 > 끄
                    else if (analizeCho[i] == (int)EnumCho.ㄱ && analizeJung[i] == (int)EnumJung.ㅗ)
                    {
                        analizeCho[i] = (int)EnumCho.ㄲ;
                        analizeJung[i] = (int)EnumJung.ㅡ;
                    }
                    // 비 > 네
                    else if (analizeCho[i] == (int)EnumCho.ㅂ && analizeJung[i] == (int)EnumJung.ㅣ)
                    {
                        analizeCho[i] = (int)EnumCho.ㄴ;
                        analizeJung[i] = (int)EnumJung.ㅔ;
                    }
                    // 유 > 윾
                    else if (analizeJung[i] == (int)EnumJung.ㅠ && analizeJong[i] == (int)EnumJong.x)
                    {
                        analizeJung[i] = (int)EnumJung.ㅡ;
                        analizeJong[i] = (int)EnumJong.ㄲ;
                    }
                }
            }

            for (int i = 0; i < analizeHangeul.Length; i++)
            {
                if (analizeHangeul[i])
                {
                    char edited = (char)(0xAC00 + 28 * 21 * analizeCho[i] + 28 * analizeJung[i] + analizeJong[i]);
                    result = result.Insert(result.Length, edited.ToString());
                }
                else
                {
                    result = result.Insert(result.Length, ((char)analizeCho[i]).ToString());
                }
            }

            return result;
        }
    }
}
