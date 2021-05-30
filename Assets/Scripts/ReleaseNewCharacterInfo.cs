using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Diamond.SkeletonDefense
{

    public class ReleaseNewCharacterInfo
    {
        public string _newCharacterName;
        public string _nextStageName;

        private static ReleaseNewCharacterInfo _releaseNewCharacterInfo;

        /// <summary>
        /// Constructor
        /// </summary>
        private ReleaseNewCharacterInfo(){}

        /// <summary>
        /// Get Instance
        /// </summary>
        /// <returns>Instance</returns>
        public static ReleaseNewCharacterInfo GetReleaseNewCharacterInfo()
        {
            if (_releaseNewCharacterInfo == null)
                _releaseNewCharacterInfo = new ReleaseNewCharacterInfo();

            return _releaseNewCharacterInfo;
        }

        public void Reset()
        {
            _releaseNewCharacterInfo = new ReleaseNewCharacterInfo();
        }
    }
}
