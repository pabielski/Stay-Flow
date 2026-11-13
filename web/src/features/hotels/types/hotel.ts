export type Hotel = {
  id: number;
  name: string;
  city: string;
  address: string;
  phone: string | null;
  email: string | null;
  checkInTime: string;
  isActive: boolean;
};
export type CreateHotelRequest = {
  name: string;
  city: string;
  address: string;
  phone: string | null;
  email: string | null;
  checkInTime: string;
};
