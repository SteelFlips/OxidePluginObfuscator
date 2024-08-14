using System;
using System.IO;
using System.Text;
using Oxide.Core;
using Oxide.Core.Plugins;
using Oxide.Game.Rust.Cui;

namespace Oxide.Plugins
{
    [Info("PluginObfuscator", "Steel", "1.1.1")]
    [Description("Command based plugin obfuscator and deobfuscator with backup functionality")]
    public class PluginObfuscator : RustPlugin
    {
        private const string BackupFolder = "Backups";

        [ConsoleCommand("obfuscate")]
        private void CmdObfuscate(ConsoleSystem.Arg arg)
        {
            if (!arg.IsAdmin)
            {
                SendReply(arg, "You do not have permission to use this command.");
                return;
            }

            if (arg.Args == null || arg.Args.Length < 2)
            {
                SendReply(arg, "Usage: obfuscate <targetname> <newname>");
                return;
            }

            string targetName = arg.Args[0];
            string newName = arg.Args[1];

            string pluginsPath = Interface.Oxide.PluginDirectory;
            string targetFilePath = Path.Combine(pluginsPath, $"{targetName}.cs");

            if (!File.Exists(targetFilePath))
            {
                SendReply(arg, $"Plugin {targetName} does not exist.");
                return;
            }

            MakeBackup(targetFilePath);
            string pluginContent = File.ReadAllText(targetFilePath);
            string obfuscatedContent = ObfuscatePluginContent(pluginContent);

            string newFilePath = Path.Combine(pluginsPath, $"{newName}.cs");
            File.WriteAllText(newFilePath, obfuscatedContent);

            SendReply(arg, $"Plugin {targetName} has been obfuscated and saved as {newName}.cs");
            Puts($"Plugin {targetName}.cs has been obfuscated and saved as {newName}.cs");
        }

        [ConsoleCommand("deobfuscate")]
        private void CmdDeobfuscate(ConsoleSystem.Arg arg)
        {
            if (!arg.IsAdmin)
            {
                SendReply(arg, "You do not have permission to use this command.");
                return;
            }

            if (arg.Args == null || arg.Args.Length < 2)
            {
                SendReply(arg, "Usage: deobfuscate <obfuscatedName> <newName>");
                return;
            }

            string obfuscatedName = arg.Args[0];
            string newName = arg.Args[1];

            string pluginsPath = Interface.Oxide.PluginDirectory;
            string obfuscatedFilePath = Path.Combine(pluginsPath, $"{obfuscatedName}.cs");

            if (!File.Exists(obfuscatedFilePath))
            {
                SendReply(arg, $"Plugin {obfuscatedName} does not exist.");
                return;
            }

            MakeBackup(obfuscatedFilePath);
            string obfuscatedContent = File.ReadAllText(obfuscatedFilePath);
            string deobfuscatedContent = DeobfuscatePluginContent(obfuscatedContent);

            string newFilePath = Path.Combine(pluginsPath, $"{newName}.cs");
            File.WriteAllText(newFilePath, deobfuscatedContent);

            SendReply(arg, $"Plugin {obfuscatedName} has been deobfuscated and saved as {newName}.cs");
            Puts($"Plugin {obfuscatedName}.cs has been deobfuscated and saved as {newName}.cs");
        }

        private void MakeBackup(string filePath)
        {
            string backupPath = Path.Combine(Interface.Oxide.PluginDirectory, BackupFolder);
            if (!Directory.Exists(backupPath))
            {
                Directory.CreateDirectory(backupPath);
            }

            string backupFilePath = Path.Combine(backupPath, Path.GetFileName(filePath) + ".bak");
            File.Copy(filePath, backupFilePath, true);
        }

        private string ObfuscatePluginContent(string content)
        {
            StringBuilder obfuscatedContent = new StringBuilder();
            foreach (char c in content)
            {
                obfuscatedContent.Append("\\u" + ((int)c).ToString("x4"));
            }
            return obfuscatedContent.ToString();
        }

        private string DeobfuscatePluginContent(string obfuscatedContent)
        {
            StringBuilder deobfuscatedContent = new StringBuilder();
            string[] parts = obfuscatedContent.Split(new string[] { "\\u" }, StringSplitOptions.RemoveEmptyEntries);
            
            foreach (string part in parts)
            {
                int charCode = int.Parse(part.Substring(0, 4), System.Globalization.NumberStyles.HexNumber);
                deobfuscatedContent.Append((char)charCode);
                
                if (part.Length > 4)
                {
                    deobfuscatedContent.Append(part.Substring(4));
                }
            }
            
            return deobfuscatedContent.ToString();
        }
    }
}
