### GitHub README 

# PluginObfuscator

**PluginObfuscator** is a Rust Oxide plugin developed to allow plugin developers to obfuscate and deobfuscate their plugins easily via console commands. Each operation creates a backup of the original file to ensure data safety.

## Features

- Obfuscate plugins to protect code.
- Deobfuscate plugins to view and modify the code.
- Automatically creates backups of the original files.

## Requirements

- Oxide for Rust

## Installation

1. Download the `PluginObfuscator.cs` file.
2. Place the file into your server's `oxide/plugins` folder.
3. Restart your Rust server or load the plugin using the console command `oxide.reload PluginObfuscator`.

## Usage

### Obfuscating a Plugin

To obfuscate a plugin, use the following command:

```
obfuscate <targetname> <newname>
```

- `<targetname>`: The name of the plugin you want to obfuscate (without the .cs extension).
- `<newname>`: The name you want to save the obfuscated file as (without the .cs extension).

Example:

```
obfuscate ExamplePlugin ObfuscatedExamplePlugin
```

### Deobfuscating a Plugin

To deobfuscate a plugin, use the following command:

```
deobfuscate <obfuscatedName> <newname>
```

- `<obfuscatedName>`: The name of the obfuscated plugin file (without the .cs extension).
- `<newname>`: The name you want to save the deobfuscated file as (without the .cs extension).

Example:

```
deobfuscate ObfuscatedExamplePlugin DeobfuscatedExamplePlugin
```

### Important Notes

- **Respect Plugin Licenses**: Some plugin developers may not allow the use of this tool to obfuscate their code. Always respect the individual plugins' license agreements and permissions before using this tool.
- **File Renaming**: While you can rename the file to whatever you want, ensure that the main class name within the plugin matches the filename. The plugin will only compile correctly if the class name remains consistent with the file name.

### Backup Folder

Backups of the original files will be stored in a `Backups` folder inside the `oxide/plugins` directory.

## Permissions

Ensure that only administrators with the required permissions can use the commands. By default, this plugin checks for admin status using `arg.IsAdmin`.

## Contributing

If you have suggestions for improvements or run into issues, please feel free to open an issue or pull request on the GitHub repository.

## License

This project is licensed under the MIT License. See the LICENSE file for details.
