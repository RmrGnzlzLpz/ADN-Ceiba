// This file can be replaced during build by using the `fileReplacements` array.
// `ng build` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

export const environment = {
  production: false,
  api_url: 'http://localhost:5000/api',
  page_size: 5,
  licensePlateValidation: /[a-zA-Z]{3}[0-9]{3}|[CcDdMmOoAa]{2}[0-9]{4}|[RrSs]{1}[0-9]{5}|[Tt]{1}[0-9]{4}|[a-zA-Z]{3}[0-9]{2}[a-zA-Z]{1}/
};

/*
 * For easier debugging in development mode, you can import the following file
 * to ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`.
 *
 * This import should be commented out in production mode because it will have a negative impact
 * on performance if an error is thrown.
 */
