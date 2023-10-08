# unsw-security-keylogger
A keylogger being developed as a UNSW project.

The C# keylogger exists within the directory 'Logger'

The C# decryptor exists within the directory 'Reader'

The C++ keylogger is in the home directory with the name "keylogger.cpp", however this is incredibly bad and not recommended.

## How To Use

### C# Keylogger

The keylogger's main functionality of sending files has been disabled, as the password to the email it connects to has since been deleted. Therefore, its purpose is that every 512 characters pressed on the keyboard, it will create a file titled "steal.bin", which is encrypted. It can be decrypted using the Reader project.

The file exists within the directory:
`Logger\bin\Debug\net7.0-windows\Keys.exe`

### Reader

To execute the reader, it must be opened in a command shell as it contains one argument, file directory. When executed it creates a file called output.bin

The file exists within the directory:
`Reader\bin\Debug\net7.0\KeyReader.exe`

#### Usage
Therefore, usage of this exe file is through the command:

`...\KeyReader.exe [directory]\steal.bin`

For example:

`Reader\bin\Debug\net7.0\KeyReader.exe Logger\bin\Debug\net7.0-windows\steal.bin`

## Example

There exists an example within each directory. Within the `Logger` directory there is an encypted file called `steal-old.bin`.

Within the `Reader` directory there exists a decrypted file of the one above, called `output-old.bin`. Both of these files can be opened in either a Notepad, or your preferred hex/binary file reading program. Personally, I have been using the website hexed.it to view these files while developing the program.