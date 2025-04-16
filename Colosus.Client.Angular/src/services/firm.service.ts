import { HttpService } from "./http.service";

export class FirmService {
  private SelectedFirm: string = '';
  constructor(private httpService: HttpService) { }

  public SelectedFirmChange(firmPublicGuid: string): void {
    this.SelectedFirm = firmPublicGuid;
  }
}
