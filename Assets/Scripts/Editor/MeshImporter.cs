using System.Linq;
using UnityEditor;
using UnityEngine;

public class MeshTextureAssigner : AssetPostprocessor
{

	void OnPreprocessModel()
	{
		ModelImporter mi = (ModelImporter)assetImporter;
		//mi.importMaterials = false;
		//mi.globalScale = 0.01f;
		mi.animationType = ModelImporterAnimationType.None;
	}

	void OnPostprocessModel(GameObject g)
	{
		g.transform.localScale = Vector3.one;
		g.transform.position = Vector3.zero;
		//var materials = g.GetComponentsInChildren<Renderer>().SelectMany(r => r.sharedMaterials);

		//foreach (var m in materials)
		//{
		//	if(m.shader.name == "Standard")
		//	{
		//		setTexture(m, "_MainTex", "_Diffuse");
		//		setTexture(m, "_MetallicGlossMap", "_Metallic");
		//		setTexture(m, "_BumpMap", "_Normal");
		//		setTexture(m, "_OcclusionMap", "_ambient_occlusion");
		//		setTexture(m, "_EmissionMap", "_Emission");
		//	}
		//}
	}

	void setTexture(Material m, string textureId, string textureSuffix)
	{
			if (m.GetTexture(textureId) == null)
			{
				var path = AssetDatabase.FindAssets(m.name + textureSuffix).FirstOrDefault();
				if(!string.IsNullOrEmpty(path))
				{
					path = AssetDatabase.GUIDToAssetPath(path);
					var tex = AssetDatabase.LoadAssetAtPath<Texture>(path);
					if (tex)
					{
						m.SetTexture(textureId, tex);
						if (textureSuffix == "_Normal")
						{
							TextureImporter importer = (TextureImporter) AssetImporter.GetAtPath(path);
							if (importer.textureType != TextureImporterType.NormalMap)
							{
								importer.textureType = TextureImporterType.NormalMap;
								AssetDatabase.WriteImportSettingsIfDirty(path);
								AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
							}
						}
						if(textureSuffix == "_Diffuse")
						{
							m.SetColor("_Color", Color.white);
						}
					}
				}
			}
	}
	
}