using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScratchCodeCompiler.Scratch
{
    internal class ScratchTarget : IScratchJsonable
    {
        public bool isStage;
        public string name;
        public List<ScratchVariable> variables;
        public List<int> lists; // TODO: Implement ScratchList
        public List<int> broadcasts; // TODO: Implement ScratchBroadcast
        public List<ScratchBlock> blocks;
        public List<int> comments; // TODO: Implement ScratchComment
        public int currentCostume;
        public List<ScratchCostume> costumes;
        public List<ScratchSound> sounds;
        public int volume;
        public int layerOrder;
        public int tempo;
        public int videoTransparency;
        public ScratchVideoState videoState;
        public int? textToSpeechLanguage = null; // TODO: Implement ScratchTextToSpeechLanguage

        public ScratchTarget(string name, bool isStage)
        {
            this.name = name;
            this.isStage = isStage;
            variables = new();
            lists = new();
            broadcasts = new();
            blocks = new();
            comments = new();
            costumes = new();
            sounds = new();
            volume = 100;
            layerOrder = 0;
            tempo = 60;
            videoTransparency = 50;
            videoState = ScratchVideoState.on;
        }

        public string ToJson()
        {
            return "";
        }
    }
}
