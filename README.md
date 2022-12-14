![Banner](https://raw.githubusercontent.com/pradosh-arduino/Chiefwood/master/res/Chiefwood.png)

# Chiefwood
  This is a file compressor and encrypter π

  [![HitCount](https://hits.dwyl.com/pradosh-arduino/Chiefwood.svg?style=flat-square&show=unique)](http://hits.dwyl.com/pradosh-arduino/Chiefwood)
  [![Say Thanks](https://img.shields.io/badge/say-thanks-ff69b4.svg?style=flat-square)](https://saythanks.io/to/pradosh-arduino)

# Usage
  - To create Chiefwood file
  ```cmd
  dotnet run -compression <level> -create <filename_without_extension>
  ```
  Compression Levels are **none, fast, optimal, best**

  **Note:** `-compression` must be first!

  - To Load Chiefwood file
  ```cmd
  dotnet run -load <filename_without_extension>
  ```
  

## Files location
  - every files that should be in Chiefwood file should be under `./contents` directory

  **Here is an Example**

  - contents/
    - test.txt
    - alsonicefilebtw.txt
    
  inside **test.txt**
  ```txt
  HERE IS AN EXAMPLE!
  Hey hi hello! normal text works!
  CAPITAL TEXT ALSO!
  Emojis also, why not ππ§ͺπ
  Even these stylish text also: ππΆππ·π ππ½πΎπ πππ
  ```
