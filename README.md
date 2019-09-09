# DesignPattten.Valetkey

This code is example of [ValetKey](https://docs.microsoft.com/en-us/azure/architecture/patterns/valet-key) design pattern. It eliminates need of providing user with direct access to storage credentials and provides Shared Access Signature token and these tokens are created using storage access policies on container, which gives control to disable token when user has done using it. 

## Nuget

Following nuget package of Azure storage SDK is used 

```bash
Azure.Storage.Blobs 12.0.0-preview.2
```

## Examples



## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

Please make sure to update tests as appropriate.

## License
[MIT](https://choosealicense.com/licenses/mit/)
