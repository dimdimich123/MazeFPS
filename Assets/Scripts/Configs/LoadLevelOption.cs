using Infrastructure;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "NewLoadLevelOption", menuName = "LoadLevelOption", order = 6)]
    public sealed class LoadLevelOption : ScriptableObject
    {
        public SceneName SceneName;
    }
}
