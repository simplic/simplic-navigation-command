del *.nupkg
nuget pack Simplic.Navigation.Command.Service.csproj -properties Configuration=Debug
nuget push *.nupkg -Source http://simplic.biz:10380/nuget