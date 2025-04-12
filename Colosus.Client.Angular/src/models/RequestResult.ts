export class RequestResult {
  OperationName: string;
  RequestToken?: string;
  Description: string;
  Data: string;
  CreateDate: Date;
  Result: ResultEnum;

  constructor(OperationName: string) {
    this.OperationName = OperationName;
    this.CreateDate = new Date();
    this.Description = '';
    this.Data = '';
    this.Result = ResultEnum.Not;
  }

  toObject(): object {
    return {
      OperationName: this.OperationName,
      RequestToken: this.RequestToken,
      Description: this.Description,
      CreateDate: this.CreateDate,
      Result: this.Result,
    };
  }
}
export enum ResultEnum {
  Ok,
  Not,
  Stoped,
  Error,
}
