using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScratchCodeCompiler.Scratch
{
    internal class ScratchCostume
    {
        public string name;
        public ImageDataFormat dataFormat;
        public ScratchId AssetId { get; }
        public string Md5ext { get; }
        public ScratchVector2 rotationCenter;

        public ScratchCostume(string name, ImageDataFormat dataFormat, ScratchVector2 rotationCenter)
        {
            this.name = name;
            this.dataFormat = dataFormat;
            AssetId = new(32, false);
            Md5ext = $"{AssetId.Id}.{dataFormat}";
            this.rotationCenter = rotationCenter;
        }
    }
}
