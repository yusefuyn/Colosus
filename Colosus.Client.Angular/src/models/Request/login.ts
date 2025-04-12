export class LoginRequestModel {
  Username: string;
  Password: string;
  HashedToPass: boolean;

  constructor(Username: string, Password: string, HashedToPass: boolean) {
    this.Username = Username;
    this.Password = Password;
    this.HashedToPass = HashedToPass;
  }

  toObject(): object {
    return {
      ToUsernameken: this.Username,
      Password: this.Password,
      HashedToPass: this.HashedToPass,
    };
  }
}
