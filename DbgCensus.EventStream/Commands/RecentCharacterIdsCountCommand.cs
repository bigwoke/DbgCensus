﻿namespace DbgCensus.EventStream.Commands
{
    /// <summary>
    /// Get a count of character ids for which events have been encountered recently.
    /// </summary>
    public record RecentCharacterIdsCountCommand() : CensusCommandBase("recentCharacterIdsCount", "event");
}
