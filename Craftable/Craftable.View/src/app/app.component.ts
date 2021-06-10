import { Component } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { CraftableService } from './config/config.service';

export interface Address {
  code: string;
  index: number;
}

export interface PostCodeAddress {
  postalcode: string;
}


export interface PostCodeAddressRanged {
  postalcode: string;
  distance_kilometer: number;
  distance_miles: number;
}


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  /**
   *
   */
  constructor(public craftableService: CraftableService) {

  }

  title = 'Craftable';

  displayedColumns: string[] = ['index', 'code'];

  dataSource: Address[] = [];

  postcodeAddress: PostCodeAddress | undefined;

  profileForm = new FormGroup({
    postcode: new FormControl('')
  });

  onSubmit() {

    this.craftableService.getAddress<PostCodeAddressRanged>(this.profileForm.value.postcode).subscribe(data => {
      this.postcodeAddress = data;
    });

    this.craftableService.getAllAddresses<PostCodeAddress[]>().subscribe(data => {
      if (data.length < 1) {
        return;
      }
      this.dataSource = data.map((val, index) => {
        return {
          code: val.postalcode,
          index: index + 1
        } as Address;
      });
    });
  }
}
