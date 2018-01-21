// Copyright 2014 Google Inc. All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace GoogleVR.HelloVR {
  using UnityEngine;

  [RequireComponent(typeof(Collider))]
  public class HoverController : MonoBehaviour {
    private Vector3 startingPosition;
    private Renderer renderer;
    public GameObject blurb;

    void Start() {
      startingPosition = transform.localPosition;
      renderer =  GetComponent<Renderer>();
      SetGazedAt(false);

    }

    public void SetGazedAt(bool gazedAt) {
      renderer = blurb.GetComponent<Renderer>();
      renderer.enabled  = gazedAt;
      Debug.Log(!gazedAt);
    }

    public void Reset() {
      int sibIdx = transform.GetSiblingIndex();
      int numSibs = transform.parent.childCount;
      for (int i=0; i<numSibs; i++) {
        GameObject sib = transform.parent.GetChild(i).gameObject;
        sib.transform.localPosition = startingPosition;
        sib.SetActive(i == sibIdx);
      }
    }

    public void Recenter() {
#if !UNITY_EDITOR
      GvrCardboardHelpers.Recenter();
#else
      if (GvrEditorEmulator.Instance != null) {
        GvrEditorEmulator.Instance.Recenter();
      }
#endif  // !UNITY_EDITOR
    }
  }
}
