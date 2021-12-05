﻿using System;
using System.Collections.Generic;
using osuTools.StoryBoard.Commands.Interface;
using osuTools.StoryBoard.Enums;
using osuTools.StoryBoard.Interfaces;
using osuTools.StoryBoard.Objects;

namespace osuTools.StoryBoard.Commands
{
    /// <summary>
    /// StoryBoard主命令
    /// </summary>
    public class StoryBoardMainCommand : IStoryBoardMainCommand
    {
        /// <inheritdoc />
        public StoryBoardResourceType ResourceType { get; set; }
        /// <inheritdoc />
        public List<IStoryBoardSubCommand> SubCommands { get; set; } = new List<IStoryBoardSubCommand>();
        /// <inheritdoc />
        public IStoryBoardResource Resource { get; set; }
        /// <inheritdoc />

        public void Parse(string line)
        {
            var ls = line.Split(new[]{','},StringSplitOptions.RemoveEmptyEntries);
            if (ls.Length == 0)
                return;
            if (line[0] != ' ')
            {
                if (ls[0] == "Sprite" || ls[0] == "4")
                    Resource = new Sprite();
                if (ls[0] == "Sample" || ls[0] == "5")
                    Resource = new Audio();
                if (ls[0] == "Animation" || ls[0] == "6")
                    Resource = new Animation();
                if (Resource is null)
                    return;
                Resource.Parse(line);
                ResourceType = Resource.ResourceType;
            }
        }
    }
}