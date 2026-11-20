import * as yup from "yup";

export const hotelSchema = yup.object({
  name: yup
    .string()
    .trim()
    .required("Nazwa jest wymagana.")
    .max(200, "Nazwa może mieć najwyżej 200 znaków."),
  city: yup
    .string()
    .trim()
    .required("Miasto jest wymagane.")
    .max(100, "Miasto może mieć najwyżej 100 znaków."),
  address: yup
    .string()
    .trim()
    .required("Adres jest wymagany.")
    .max(300, "Adres może mieć najwyżej 300 znaków."),
  phone: yup
    .string()
    .trim()
    .max(30, "Telefon może mieć najwyżej 30 znaków.")
    .default(""),
  email: yup
    .string()
    .trim()
    .email("Niepoprawny adres e-mail.")
    .max(254, "E-mail może mieć najwyżej 254 znaki.")
    .default(""),
  checkInTime: yup.string().required("Godzina zameldowania jest wymagana."),
});

export type HotelFormValues = yup.InferType<typeof hotelSchema>;
