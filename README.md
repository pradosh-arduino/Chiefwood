![Banner](https://raw.githubusercontent.com/pradosh-arduino/Chiefwood/master/res/Chiefwood.png)

# Chiefwood
  This is a gzip like tool but with no compressibility for now 📚 (It can encrypt files)

  [![HitCount](https://hits.dwyl.com/pradosh-arduino/Chiefwood.svg?style=flat-square&show=unique)](http://hits.dwyl.com/pradosh-arduino/Chiefwood)

# Usage
  - To create Chiefwood file
  ```cmd
  dotnet run -create <filename_without_extension>
  ```
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