export const environment = {
  production: true,
 msalConfig: {
    auth: {
      clientId: '95fe6126-b3bc-4611-86a9-024db3a83db6',
      authority:
        'https://login.microsoftonline.com/db71a4d9-f264-488b-8ba3-b07c26caea6a',
    },
  },
  apiConfig: {
    scopes: ['user.read'],
    uri: 'https://graph.microsoft.com/v1.0/me',
    catalogApiUri: 'https://bigdreamsapps-new-ewanhuhyb4gsfddh.eastus-01.azurewebsites.net/api/',
  },
};
