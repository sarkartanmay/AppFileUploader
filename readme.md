# App File Uploader

## OpenTelemetry
### Aspire Dashboard

```
APP_UPD_NAME=DckUpdApp
APP_UPD_INFRA_LOG_OTLP_GRPC=http://aspd_aspire-dashboard:18889
```

- Note : Aspire Internal GRPC Port number required

### Seq Dashboard


```
APP_UPD_INFRA_LOG_SEQ=http://local_seq_seq
```
- Note : Port number not required

## Infrastructure

### File


| Type     | Value    |
| ----------------- | ------ |
| Local | OnPrem |
| Azure Blob | AzureBlob |

