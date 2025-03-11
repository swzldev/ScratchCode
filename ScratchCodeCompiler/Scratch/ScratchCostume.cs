using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScratchCodeCompiler.Scratch
{
    internal class ScratchCostume : IScratchJsonable
    {
        public string name;
        public ImageDataFormat dataFormat;
        public ScratchAssetId AssetId { get; }
        public ScratchVector2 rotationCenter;

        public ScratchCostume(string name, ImageDataFormat dataFormat, ScratchVector2 rotationCenter)
        {
            this.name = name;
            this.dataFormat = dataFormat;
            AssetId = new();
            this.rotationCenter = rotationCenter;
        }

        public string ToJson()
        {
            // "name":"backdrop1","dataFormat":"svg","assetId":"cd21514d0531fdffb22204e0ec5ed84a","md5ext":"cd21514d0531fdffb22204e0ec5ed84a.svg","rotationCenterX":240,"rotationCenterY":180
            string json = "{";
            json += $"\"name\":\"{name}\",";
            json += $"\"dataFormat\":\"{dataFormat.ToString().ToLower()}\",";
            json += $"\"assetId\":\"{AssetId.Id}\",";
            json += $"\"md5ext\":\"{AssetId.Md5}\",";
            json += $"\"rotationCenterX\":{rotationCenter.x},";
            json += $"\"rotationCenterY\":{rotationCenter.y}";
            json += "}";
            return json;
        }
    }
}
