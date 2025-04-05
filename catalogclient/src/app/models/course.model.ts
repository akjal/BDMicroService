export interface Course {
  name: string;
  id: string;
  duration: number;
  type: string;
  description: string;
  currentEnrollment: number;
  maxEnrollment: number;
  universityId: string;
  universityName?: string;
  intakeMonths: string;
}
