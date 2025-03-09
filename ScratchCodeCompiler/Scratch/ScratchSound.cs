using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScratchCodeCompiler.Scratch
{
    internal class ScratchSound
    {
        public string name;
        public string assetId;
        public SoundDataFormat dataFormat;
        public string format; // Always ""
        public int rate;
        public int sampleCount;
        public string md5ext;

        public ScratchSound(string name, SoundDataFormat dataFormat, int rate, int sampleCount)
        {
            this.name = name;
            this.dataFormat = dataFormat;
            this.rate = rate;
            this.sampleCount = sampleCount;
            md5ext = "";
        }
    }
}
