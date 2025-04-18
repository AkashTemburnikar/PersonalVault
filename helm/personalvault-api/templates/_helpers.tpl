{{- define "personalvault-api.name" -}}
personalvault-api
{{- end }}

{{- define "personalvault-api.fullname" -}}
{{ .Release.Name }}-{{ include "personalvault-api.name" . }}
{{- end }}

{{- define "personalvault-api.labels" -}}
app.kubernetes.io/name: {{ include "personalvault-api.name" . }}
app.kubernetes.io/instance: {{ .Release.Name }}
{{- end }}