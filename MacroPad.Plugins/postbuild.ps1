#$BuildRoot = Split-Path -Path $args[0] -Parent
#$BuildRoot = Join-Path -Path $BuildRoot -ChildPath "*.dll"
#$DllFiles = Split-Path -Path $BuildRoot -Leaf -Resolve
#echo "Args: $args..."
#echo "BuildRoot: $BuildRoot..."
#echo "DllFiles: $DllFiles..."
$DllFile = Split-Path -Path $args -Leaf
$FileDest = Join-Path -Path "D:\Documents\Desktop\MacroPad\MacroPad\ConsoleApp1\bin\Debug\net7.0\plugins\MacroPad.Plugins.Protocol.Midi" -ChildPath $DllFile
$FileDest = Join-Path -Path "D:\Documents\Desktop\MacroPad\MacroPad\MacroPad\bin\Debug\net7.0\plugins\MacroPad.Plugins.Protocol.Midi" -ChildPath $DllFile
echo "Copying $args to $FileDest..."
Copy-Item -Path $args -Destination $FileDest -Force