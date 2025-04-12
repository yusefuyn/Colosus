import { sha256 } from 'js-sha256';


export class RequestParameter {
  Token?: string;
  RequestToken?: string;
  Supply?: number;
  Address?: string;

  CreateDate: Date;

  Data: string;
  RequestParameterHash: string;

  constructor(Data: string) {
    this.Data = Data;
    this.CreateDate = new Date();
    this.RequestParameterHash = sha256('test');
    this.Token = '';
    this.RequestToken = '';
  }

  toObject(): object {
    return {
      Token: this.Token,
      RequestToken: this.RequestToken,
      Supply: this.Supply,
      Data: this.Data,
      RequestParameterHash: this.RequestParameterHash,
      Address: this.Address,
    };
  }
}
