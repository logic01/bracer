export const RouteUrls = {
  LoginComponent: 'login',
  BillingDashboardComponent: 'billing',

  VendorIntakeDocumentComponent: 'vendor/:vendorId/intake-document/:intakeFormId',
  PhysicianIntakeDocumentComponent: 'physician/:physicianId/intake-document/:intakeFormId',

  AgentDashboardComponent: 'agent',

  AgentEditComponent: 'agent/edit/:id',
  AgentCreateComponent: 'account/create/agent',

  PatientCreateComponent: 'agent/:agentId/patient',
  PatientEditComponent: 'agent/:agentId/patient/:patientId',

  CreatePainDmeOnlyComponent: 'patient/:patientId/pain-dme-only',
  EditPainDmeOnlyComponent: 'patient/:patientId/pain-dme-only/:intakeFormId/edit',

  PhysicianDashboardComponent: 'physician',
  PhysicianCreateComponent: 'account/create/physician',
  PhysicianEditComponent: 'physician/edit/:id',

  AdminDashboardComponent: 'admin',
  AdminCreateComponent: 'account/create/admin',
  AdminEditComponent: 'admin/edit/:id',

  VendorCreateComponent: 'admin/create/vendor',
  VendorEditComponent: 'vendor/:id/edit',
  VendorViewComponent: 'vendor/:id/view'
};
