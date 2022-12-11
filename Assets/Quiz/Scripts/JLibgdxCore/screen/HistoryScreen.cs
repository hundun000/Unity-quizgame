using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HistoryScreen : BaseHundunScreen
{

    List<MatchHistoryDTO> histories { get; set; }

    HistoryAreaVM historyAreaVM;

    public class MatchHistoryDTO
    {
        public Dictionary<String, int> data { get; set; }
    }
}
