# SpriteCreator

Version 1.1


#### Simple software that create a big image sprite from a series of images in a folder

## Installation

- Run the `sprite-creator.msi` file at the root of the repro.
- The installer will install .NET Framework 4.0 Client Profile if it's not on your computer already.
- Add the installation folder (by default `C:\Program Files (x86)\Deux Huit Huit\SpriteCreator`) to the system PATH.
- Open the command prompt.
- Navgate to the folder containing your images.
- type `sprite` and ENTER.

## Parameters

Optionally, you can pass parameters to the `sprite` command

- **-f:name-of-the-sprite-file.ext** the name of the created image
	- Defaults to sprite.png
- **-filter:*.ext** a filter string for filtering the images in the folder
	- Defaults to *.png
- **-t:path\to\ouput\folder\** the folder where to save the sprite image
	- Defaults to the current directory
- **-hl:number-of-columns-in-sprite** the images will be added horizontally for the number of columns specified
	- Defaults to 1
- Example with jpg: `sprite -f:sprite.jpg -filter:*.jpg -hl:1`

## Build your own version

You can checkout the entire repro and open the Visual Studio 2010 solution.
	
## Copyrights

- See [LICENSE.txt](https://github.com/DeuxHuitHuit/SpriteCreator/blob/master/LICENSE.txt) for details
- [Deux Huit Huit](http://www.deuxhuithuit.com)

*Voila !*
