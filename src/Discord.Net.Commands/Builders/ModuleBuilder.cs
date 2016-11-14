﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Discord.Commands
{
    public class ModuleBuilder
    {
        private List<CommandBuilder> commands;
        private List<ModuleBuilder> submodules;
        private List<string> aliases;


        public ModuleBuilder()
            : this(null, null)
        { }

        public ModuleBuilder(string prefix)
            : this(null, prefix)
        { }

        internal ModuleBuilder(ModuleBuilder parent)
            : this(parent, null)
        { }

        internal ModuleBuilder(ModuleBuilder parent, string prefix)
        {
            commands = new List<CommandBuilder>();
            submodules = new List<ModuleBuilder>();
            aliases = new List<string>();

            if (prefix != null)
            {
                aliases.Add(prefix);
                Name = prefix;
            }
            
            ParentModule = parent;
        }


        public string Name { get; set; }
        public string Summary { get; set; }
        public string Remarks { get; set; }
        public ModuleBuilder ParentModule { get; }

        public List<CommandBuilder> Commands => commands;
        public List<ModuleBuilder> Modules => submodules;
        public List<string> Aliases => aliases;


        public ModuleBuilder SetName(string name)
        {
            Name = name;
            return this;
        }

        public ModuleBuilder SetSummary(string summary)
        {
            Summary = summary;
            return this;
        }

        public ModuleBuilder SetRemarks(string remarks)
        {
            Remarks = remarks;
            return this;
        }

        public ModuleBuilder AddAlias(string alias)
        {
            aliases.Add(alias);
            return this;
        }

        public CommandBuilder AddCommand() => AddCommand(null);
        public CommandBuilder AddCommand(string name)
        {
            var builder = new CommandBuilder(this, name);
            commands.Add(builder);

            return builder;
        }

        public ModuleBuilder AddSubmodule() => AddSubmodule(null);
        public ModuleBuilder AddSubmodule(string prefix)
        {
            var builder = new ModuleBuilder(this, prefix);
            submodules.Add(builder);

            return builder;
        }

        public ModuleBuilder Done()
        {
            if (ParentModule == null)
                throw new InvalidOperationException("Cannot finish a top-level module!");

            return ParentModule;
        }
    }
}
